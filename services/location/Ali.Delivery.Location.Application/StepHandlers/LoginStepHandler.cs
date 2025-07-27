using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Constants;
using Ali.Delivery.Location.Application.Models;

namespace Ali.Delivery.Location.Application.StepHandlers;

/// <summary>
/// Представляет обработчик для шага аутентификации пользователя ("Authorization").
/// Его задача — получить учетные данные от пользователя, передать их сервису аутентификации
/// и вернуть результат для перехода в следующее состояние.
/// </summary>
public class LoginStepHandler : IStepHandler
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IAppDbContext _dbContext;
    private readonly INotificationService _notification;
    private readonly IUserLanguageService _userLanguageService;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="LoginStepHandler" />.
    /// </summary>
    /// <param name="notification">Сервис для отправки уведомлений пользователю.</param>
    /// <param name="authenticationService">Сервис для выполнения логики аутентификации.</param>
    public LoginStepHandler(INotificationService notification, IAuthenticationService authenticationService, IAppDbContext dbContext, IUserLanguageService userLanguageService)
    {
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
        _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _userLanguageService = userLanguageService ?? throw new ArgumentNullException(nameof(userLanguageService));
    }

    /// <inheritdoc />
    public async Task<HandlerResult> HandleAsync(MessageInfo messageInfo, CancellationToken cancellationToken = default)
    {
        var chatId = messageInfo.ChatId;

        if (chatId == 0)
        {
            return new HandlerResult(string.Empty);
        }

        if (messageInfo is not { Text: { } text })
        {
            await _notification.SendNotificationMessageAsync(chatId, NotificationType.InvalidAuthCommand, cancellationToken: cancellationToken);
            return new HandlerResult(string.Empty);
        }

        if (string.Equals(text, Commands.Stop, StringComparison.OrdinalIgnoreCase))
        {
            await _notification.SendNotificationMessageAsync(messageInfo.ChatId, NotificationType.SessionEnded, cancellationToken: cancellationToken);
            return new HandlerResult(Steps.Stop);
        }

        var res = await _authenticationService.LoginAsync(chatId, text, cancellationToken);

        var language = await _userLanguageService.GetUserLanguageAsync(chatId);

        if (language == null)
        {
            throw new ArgumentException(nameof(language));
        }

        await _userLanguageService.UpsertUserLanguageAsync(chatId, language, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new HandlerResult(res.NextStepKey);
    }
}
