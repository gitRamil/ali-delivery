using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Models;

namespace Ali.Delivery.Location.Application.StepHandlers;

public class StartStepHandler : IStepHandler
{
    private readonly INotificationService _notification;

    public StartStepHandler(INotificationService notification) => _notification = notification ?? throw new ArgumentNullException(nameof(notification));

    public async Task<HandlerResult> HandleAsync(MessageInfo messageInfo, CancellationToken cancellationToken = default)
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
            case "/start":
                await _notification.SendNotificationMessageAsync(messageInfo.ChatId, NotificationType.N1_Welcome);
                return new HandlerResult(string.Empty);
            case "/login":
                await _notification.SendNotificationMessageAsync(messageInfo.ChatId, NotificationType.N0_EnterCredentials);
                return new HandlerResult("Authorization");
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

        await _notification.SendNotificationMessageAsync(chatId, NotificationType.N1_InvalidAuthCommand);
    }
}
