using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;
using Ali.Delivery.Location.Infrastructure.ExternalServices.Models.Configuration;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Telegram.Bot;

namespace Ali.Delivery.Location.Infrastructure.StepHandlers;

public class StartStepHandler : IStepHandler
{
    private readonly ITelegramBotClient _bot;
    private readonly INotificationService _notification;

    public StartStepHandler(ITelegramBotClient bot, INotificationService notification)
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
            case "/start":
                await _bot.SendMessage(messageInfo.ChatId!, _notification.GenerateNotificationMessage(NotificationType.N1_Welcome));
                return new HandlerResult(string.Empty);
            case "/login":
                await _bot.SendMessage(messageInfo.ChatId!, _notification.GenerateNotificationMessage(NotificationType.N0_EnterCredentials));
                return new HandlerResult("Authorization");
            default:
                await SendInvalid(messageInfo);
                return new HandlerResult(string.Empty);
        }
    }

    private async Task SendInvalid(MessageInfo messageInfo)
    {
        var chatId = messageInfo.ChatId;

        await _bot.SendMessage(chatId, _notification.GenerateNotificationMessage(NotificationType.N1_InvalidAuthCommand));
    }
}
