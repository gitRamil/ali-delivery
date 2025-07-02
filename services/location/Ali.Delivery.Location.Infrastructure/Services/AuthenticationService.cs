using System.Net;
using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Constants;
using Ali.Delivery.Location.Application.Models;
using Ali.Delivery.Location.Application.Models.Authentication;
using Microsoft.Extensions.Logging;
using Refit;

namespace Ali.Delivery.Location.Infrastructure.Services;

/// <summary>
/// Представляет реализацию сервиса аутентификации.
/// Этот сервис оркестрирует процесс входа, используя <see cref="IFileServiceForOrder" /> для взаимодействия
/// с внешним API и управляя состоянием пользователя и уведомлениями.
/// </summary>
public class AuthenticationService : IAuthenticationService
{
    private readonly IFileServiceForOrder _api;
    private readonly ILogger<AuthenticationService> _logger;
    private readonly INotificationService _notification;
    private readonly IUserStateService _userStateService;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="AuthenticationService" />.
    /// </summary>
    /// <param name="api">Refit-клиент для отправки запросов к внешнему сервису.</param>
    /// <param name="logger">Логгер для записи событий и ошибок.</param>
    /// <param name="userStateService">Сервис для управления состоянием пользователя (логин, текущий шаг).</param>
    /// <param name="notification">Сервис для отправки уведомлений пользователю.</param>
    public AuthenticationService(IFileServiceForOrder api, ILogger<AuthenticationService> logger, IUserStateService userStateService, INotificationService notification)
    {
        _api = api ?? throw new ArgumentNullException(nameof(api));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userStateService = userStateService ?? throw new ArgumentNullException(nameof(userStateService));
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
    }

    /// <inheritdoc />
    /// <remarks>
    /// Этот метод выполняет полный цикл проверки учетных данных:
    /// 1. Пытается получить токен доступа через <see cref="IFileServiceForOrder.LoginAsync" />.
    /// 2. Если токен получен, проверяет его валидность, запрашивая данные пользователя через
    /// <see cref="IFileServiceForOrder.GetCurrentUserAsync" />.
    /// 3. Обрабатывает различные сценарии, включая неверные учетные данные (401 Unauthorized), необходимость регистрации и
    /// другие ошибки сети.
    /// </remarks>
    public async Task<AuthenticationResult> AuthenticateAsync(string login, string password)
    {
        try
        {
            var token = await _api.LoginAsync(new LoginRequest(login, password));

            if (string.IsNullOrWhiteSpace(token))
            {
                _logger.LogWarning("Empty token for {Login}", login);
                return new AuthenticationResult(AuthResult.InvalidCredentials);
            }

            var user = await _api.GetCurrentUserAsync($"Bearer {token}");

            if (string.IsNullOrWhiteSpace(user?.Login))
            {
                _logger.LogWarning("User must register: {Login}", login);
                return new AuthenticationResult(AuthResult.RegistrationRequired);
            }

            _logger.LogInformation("Login OK: {Login}", user.Login);
            return new AuthenticationResult(AuthResult.Success);
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            _logger.LogWarning("401 for {Login}", login);
            return new AuthenticationResult(AuthResult.InvalidCredentials);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Auth error for {Login}", login);
            return new AuthenticationResult(AuthResult.Error);
        }
    }

    /// <inheritdoc />
    /// <remarks>
    /// В отличие от <see cref="AuthenticateAsync" />, который выполняет только проверку,
    /// этот метод управляет всем процессом взаимодействия с пользователем в чате:
    /// 1. Парсит текст сообщения для извлечения логина и пароля.
    /// 2. Вызывает <see cref="AuthenticateAsync" /> для проверки данных.
    /// 3. В зависимости от результата, отправляет пользователю соответствующее уведомление.
    /// 4. Сохраняет состояние пользователя (логин) в случае успеха.
    /// 5. Возвращает ключ для перехода в следующее состояние конечного автомата.
    /// </remarks>
    public async Task<CommandResult> LoginAsync(long chatId, string text)
    {
        var parts = text.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length < 2)
        {
            await _notification.SendNotificationMessageAsync(chatId, NotificationType.N0_EnterCredentials);
            return new CommandResult(string.Empty);
        }

        var (login, password) = (parts[0], parts[1]);
        var auth = await AuthenticateAsync(login, password);

        switch (auth.Status)
        {
            case AuthResult.Success:
                await _userStateService.SetUserLoginAsync(chatId, login);
                await _notification.SendNotificationMessageAsync(chatId, NotificationType.N4_AuthenticationComplete);
                return new CommandResult(Steps.AuthComplete);

            case AuthResult.InvalidCredentials:
                await _notification.SendNotificationMessageAsync(chatId, NotificationType.N2_InvalidCredentials);
                return new CommandResult(string.Empty);

            default:
                await _notification.SendNotificationMessageAsync(chatId, NotificationType.N1_InvalidCommand);
                return new CommandResult(string.Empty);
        }
    }
}
