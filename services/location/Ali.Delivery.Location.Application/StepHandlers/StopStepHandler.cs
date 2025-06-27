using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Models;

namespace Ali.Delivery.Location.Application.StepHandlers;

public sealed class StopStepHandler : IStepHandler
{
    private readonly INotificationService _notification;

    public StopStepHandler(INotificationService notification) => _notification = notification ?? throw new ArgumentNullException(nameof(notification));

    public async Task<HandlerResult> HandleAsync(MessageInfo messageInfo)
    {
        if (messageInfo is not { Text: { } text })
        {
            await SendInvalid(messageInfo);
            return new HandlerResult(string.Empty);
        }

        if (text.Trim()
                .Equals("/start", StringComparison.OrdinalIgnoreCase))
        {
            return new HandlerResult("StartStep");
        }

        await SendInvalid(messageInfo);

        return new HandlerResult(string.Empty);
    }

    private async Task SendInvalid(MessageInfo messageInfo)
    {
        await _notification.SendNotificationMessageAsync(messageInfo.ChatId, NotificationType.N1_InvalidCommand);
    }
}
