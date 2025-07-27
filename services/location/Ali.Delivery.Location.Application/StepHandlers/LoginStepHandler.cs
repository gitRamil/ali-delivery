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

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="LoginStepHandler" />.
    /// </summary>
    /// <param name="notification">Сервис для отправки уведомлений пользователю.</param>
    /// <param name="authenticationService">Сервис для выполнения логики аутентификации.</param>
    /// <param name="dbContext">Контекст базы данных.</param>
    public LoginStepHandler(INotificationService notification, IAuthenticationService authenticationService, IAppDbContext dbContext)
    {
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
        _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
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

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new HandlerResult(res.NextStepKey);
    }
}
