using Ali.Delivery.Location.Application.Abstractions;
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
    private readonly INotificationService _notification;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="LoginStepHandler"/>.
    /// </summary>
    /// <param name="notification">Сервис для отправки уведомлений пользователю.</param>
    /// <param name="authenticationService">Сервис для выполнения логики аутентификации.</param>
    public LoginStepHandler(INotificationService notification, IAuthenticationService authenticationService)
    {
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
        _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
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
            await _notification.SendNotificationMessageAsync(chatId, NotificationType.N1_InvalidAuthCommand, cancellationToken: cancellationToken);
            return new HandlerResult(string.Empty);
        }

        if (string.Equals(text, "/stop", StringComparison.OrdinalIgnoreCase))
        {
            await _notification.SendNotificationMessageAsync(messageInfo.ChatId, NotificationType.N_SessionEnded, cancellationToken: cancellationToken);
            return new HandlerResult("StopStep");
        }

        var res = await _authenticationService.LoginAsync(chatId, text);

        return new HandlerResult(res.NextStepKey);
    }
}
