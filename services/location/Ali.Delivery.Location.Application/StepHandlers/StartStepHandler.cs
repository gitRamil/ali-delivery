using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Constants;
using Ali.Delivery.Location.Application.Extensions;
using Ali.Delivery.Location.Application.Models;

namespace Ali.Delivery.Location.Application.StepHandlers;

/// <summary>
/// Представляет обработчик для начального состояния ("Start").
/// Он отвечает за обработку первых команд пользователя, таких как /start и /login,
/// и инициирует переход к следующим шагам.
/// </summary>
public class StartStepHandler : IStepHandler
{
    private readonly INotificationService _notification;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="StartStepHandler" />.
    /// </summary>
    /// <param name="notification">Сервис для отправки уведомлений пользователю.</param>
    public StartStepHandler(INotificationService notification) => _notification = notification ?? throw new ArgumentNullException(nameof(notification));

    /// <inheritdoc />
    public async Task<HandlerResult> HandleAsync(MessageInfo messageInfo, CancellationToken cancellationToken = default)
    {
        var chatId = messageInfo.ChatId;

        if (messageInfo is not { Text: { } text })
        {
            await SendInvalid(messageInfo, cancellationToken);
            return new HandlerResult(string.Empty);
        }

        switch (text.MessageToCommand())
        {
            case Commands.Start:
                await _notification.SendNotificationMessageAsync(chatId, NotificationType.Welcome, cancellationToken: cancellationToken);
                return new HandlerResult(string.Empty);
            case Commands.Login:
                await _notification.SendNotificationMessageAsync(chatId, NotificationType.EnterCredentials, cancellationToken: cancellationToken);
                return new HandlerResult(Steps.Authorization);
            case Commands.Stop:
                await _notification.SendNotificationMessageAsync(chatId, NotificationType.SessionEnded, cancellationToken: cancellationToken);
                return new HandlerResult(Steps.Stop);
            default:
                await SendInvalid(messageInfo, cancellationToken);
                return new HandlerResult(string.Empty);
        }
    }

    private async Task SendInvalid(MessageInfo messageInfo, CancellationToken cancellationToken) =>
        await _notification.SendNotificationMessageAsync(messageInfo.ChatId, NotificationType.InvalidAuthCommand, cancellationToken: cancellationToken);
}
