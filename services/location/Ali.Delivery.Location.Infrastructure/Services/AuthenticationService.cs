using System.Net;
using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Constants;
using Ali.Delivery.Location.Application.Models;
using Ali.Delivery.Location.Application.Models.Authentication;
using Ali.Delivery.Location.Domain.Entities;
using Microsoft.EntityFrameworkCore;
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
    private readonly IAppDbContext _dbContext;
    private readonly ILogger<AuthenticationService> _logger;
    private readonly INotificationService _notification;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="AuthenticationService" />.
    /// </summary>
    /// <param name="api">Refit-клиент для отправки запросов к внешнему сервису.</param>
    /// <param name="logger">Логгер для записи событий и ошибок.</param>
    /// <param name="notification">Сервис для отправки уведомлений пользователю.</param>
    /// <param name="dbContext">Контекст БД.</param>
    public AuthenticationService(IFileServiceForOrder api, ILogger<AuthenticationService> logger, INotificationService notification, IAppDbContext dbContext)
    {
        _api = api ?? throw new ArgumentNullException(nameof(api));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <inheritdoc />
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
            return new AuthenticationResult(AuthResult.Success, user.Id);
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
    public async Task<CommandResult> LoginAsync(long chatId, string text, CancellationToken cancellationToken)
    {
        var parts = text.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length < 2)
        {
            await _notification.SendNotificationMessageAsync(chatId, NotificationType.EnterCredentials, cancellationToken: cancellationToken);
            return new CommandResult(string.Empty);
        }

        var (login, password) = (parts[0], parts[1]);
        var auth = await AuthenticateAsync(login, password);

        switch (auth.Status)
        {
            case AuthResult.Success:

                await _notification.SendNotificationMessageAsync(chatId, NotificationType.AuthenticationComplete, cancellationToken: cancellationToken);
                var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.ChatId == chatId.ToString(), cancellationToken);

                if (existingUser != null)
                {
                    return new CommandResult(Steps.AuthComplete);
                }

                var user = new User(auth.UserId, login, chatId.ToString());
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return new CommandResult(Steps.AuthComplete);

            case AuthResult.InvalidCredentials:
                await _notification.SendNotificationMessageAsync(chatId, NotificationType.InvalidCredentials, cancellationToken: cancellationToken);
                return new CommandResult(string.Empty);

            default:
                await _notification.SendNotificationMessageAsync(chatId, NotificationType.InvalidCommand, cancellationToken: cancellationToken);
                return new CommandResult(string.Empty);
        }
    }
}
