using Ali.Delivery.Location.Infrastructure.Interfaces;
using Ali.Delivery.Location.Infrastructure.Models;
using Telegram.Bot;

namespace Ali.Delivery.Location.Infrastructure.StepHandlers;

public sealed class StopStepHandler : IStepHandler
{
    private readonly ITelegramBotClient _bot;
    private readonly INotificationService _notification;

    public StopStepHandler(ITelegramBotClient bot, INotificationService notification)
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
        var chatId = messageInfo.ChatId;

        if (chatId != 0)
        {
            await _bot.SendMessage(chatId!, _notification.GenerateNotificationMessage(NotificationType.N1_InvalidCommand));
        }
    }
}
