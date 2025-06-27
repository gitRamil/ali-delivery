using Ali.Delivery.Location.Application.Interfaces;
using Ali.Delivery.Location.Application.Models;

namespace Ali.Delivery.Location.Application.StepHandlers;

public sealed class AuthCompleteStepHandler : IStepHandler
{
    private readonly INotificationService _notification;

    public AuthCompleteStepHandler(INotificationService notification) => _notification = notification ?? throw new ArgumentNullException(nameof(notification));

    public async Task<HandlerResult> HandleAsync(MessageInfo messageInfo)
    {
        if (messageInfo is not { Text: { } text })
        {
            await SendInvalid(messageInfo);
            return new HandlerResult(string.Empty);
        }

        text = text.Trim()
                   .ToLowerInvariant();

        switch (text)
        {
            case "/geosharing":
                await _notification.SendNotificationMessageAsync(messageInfo.ChatId, NotificationType.N5_RequestLocation);
                return new HandlerResult("GeoSharing");
            case "/stop":
                await _notification.SendNotificationMessageAsync(messageInfo.ChatId, NotificationType.N_SessionEnded);
                return new HandlerResult("StopStep");
            default:
                await SendInvalid(messageInfo);
                return new HandlerResult(string.Empty);
        }
    }

    private async Task SendInvalid(MessageInfo messageInfo)
    {
        var chatId = messageInfo.ChatId;

        await _notification.SendNotificationMessageAsync(chatId, NotificationType.N1_InvalidCommand);
    }
}
