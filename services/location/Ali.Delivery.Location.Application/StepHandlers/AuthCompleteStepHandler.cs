using Ali.Delivery.Location.Infrastructure.Interfaces;
using Ali.Delivery.Location.Infrastructure.Models;
using Telegram.Bot;

namespace Ali.Delivery.Location.Infrastructure.StepHandlers;

public sealed class AuthCompleteStepHandler : IStepHandler
{
    private readonly ITelegramBotClient _bot;
    private readonly INotificationService _notification;

    public AuthCompleteStepHandler(ITelegramBotClient bot, INotificationService notification)
    {
        _bot = bot ?? throw new ArgumentNullException(nameof(bot));
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
    }

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
                await _bot.SendMessage(messageInfo.ChatId, _notification.GenerateNotificationMessage(NotificationType.N5_RequestLocation));
                return new HandlerResult("GeoSharing");
            case "/stop":
                await _bot.SendMessage(messageInfo.ChatId, _notification.GenerateNotificationMessage(NotificationType.N_SessionEnded));
                return new HandlerResult("StopStep");
            default:
                await SendInvalid(messageInfo);
                return new HandlerResult(string.Empty);
        }
    }

    private async Task SendInvalid(MessageInfo messageInfo)
    {
        var chatId = messageInfo.ChatId;

        await _bot.SendMessage(chatId, _notification.GenerateNotificationMessage(NotificationType.N1_InvalidCommand));
    }
}
