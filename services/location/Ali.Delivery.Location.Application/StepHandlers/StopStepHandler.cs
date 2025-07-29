using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Constants;
using Ali.Delivery.Location.Application.Extensions;
using Ali.Delivery.Location.Application.Models;

namespace Ali.Delivery.Location.Application.StepHandlers;

/// <summary>
/// Представляет обработчик для конечного состояния ("Stop").
/// Этот шаг завершает сессию пользователя и ожидает команду для начала нового цикла (например, /start).
/// </summary>
public sealed class StopStepHandler : IStepHandler
{
    private readonly INotificationService _notification;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="StopStepHandler" />.
    /// </summary>
    /// <param name="notification">Сервис для отправки уведомлений пользователю.</param>
    public StopStepHandler(INotificationService notification) => _notification = notification ?? throw new ArgumentNullException(nameof(notification));

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
                return new HandlerResult(Steps.Start);
            default:
                await SendInvalid(messageInfo, cancellationToken);
                return new HandlerResult(string.Empty);
        }
    }

    private async Task SendInvalid(MessageInfo messageInfo, CancellationToken cancellationToken) =>
        await _notification.SendNotificationMessageAsync(messageInfo.ChatId, NotificationType.InvalidCommand, cancellationToken: cancellationToken);
}
