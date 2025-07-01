using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Models;

namespace Ali.Delivery.Location.Application.StepHandlers;

/// <summary>
/// Представляет обработчик для конечного состояния ("StopStep").
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
        if (messageInfo is not { Text: { } text })
        {
            await SendInvalid(messageInfo, cancellationToken);
            return new HandlerResult(string.Empty);
        }

        if (text.Trim()
                .Equals("/start", StringComparison.OrdinalIgnoreCase))
        {
            return new HandlerResult("StartStep");
        }

        await SendInvalid(messageInfo, cancellationToken);
        return new HandlerResult(string.Empty);
    }

    private async Task SendInvalid(MessageInfo messageInfo, CancellationToken cancellationToken)
    {
        await _notification.SendNotificationMessageAsync(messageInfo.ChatId, NotificationType.N1_InvalidCommand, cancellationToken: cancellationToken);
    }
}
