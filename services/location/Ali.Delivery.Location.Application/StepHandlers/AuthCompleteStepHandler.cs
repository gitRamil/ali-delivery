using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Constants;
using Ali.Delivery.Location.Application.Models;

namespace Ali.Delivery.Location.Application.StepHandlers;

/// <summary>
/// Представляет обработчик для состояния "AuthComplete".
/// На этом шаге пользователь уже аутентифицирован и может выбирать дальнейшие действия,
/// такие как начало отслеживания геолокации или выход из системы.
/// </summary>
public sealed class AuthCompleteStepHandler : IStepHandler
{
    private readonly INotificationService _notification;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="AuthCompleteStepHandler" />.
    /// </summary>
    /// <param name="notification">Сервис для отправки уведомлений пользователю.</param>
    public AuthCompleteStepHandler(INotificationService notification) => _notification = notification ?? throw new ArgumentNullException(nameof(notification));

    /// <inheritdoc />
    public async Task<HandlerResult> HandleAsync(MessageInfo messageInfo, CancellationToken cancellationToken = default)
    {
        if (messageInfo is not { Text: { } text })
        {
            await SendInvalid(messageInfo, cancellationToken);
            return new HandlerResult(string.Empty);
        }

        text = text.Trim()
                   .ToLowerInvariant();

        switch (text)
        {
            case Commands.Geosharing:
                await _notification.SendNotificationMessageAsync(messageInfo.ChatId, NotificationType.N5_RequestLocation, cancellationToken: cancellationToken);
                return new HandlerResult(Steps.GeoSharing);
            case Commands.Stop:
                await _notification.SendNotificationMessageAsync(messageInfo.ChatId, NotificationType.N_SessionEnded, cancellationToken: cancellationToken);
                return new HandlerResult(Steps.Stop);
            default:
                await SendInvalid(messageInfo, cancellationToken);
                return new HandlerResult(string.Empty);
        }
    }

    private async Task SendInvalid(MessageInfo messageInfo, CancellationToken cancellationToken = default)
    {
        var chatId = messageInfo.ChatId;
        await _notification.SendNotificationMessageAsync(chatId, NotificationType.N1_InvalidCommand, cancellationToken: cancellationToken);
    }
}
