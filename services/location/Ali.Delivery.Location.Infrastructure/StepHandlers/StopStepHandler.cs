using Ali.Delivery.Location.Infrastructure.ExternalServices.Models.Configuration;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

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

    public async Task<HandlerResult> HandleAsync(Update update)
    {
        if (update.Message is not { Text: { } text })
        {
            await SendInvalid(update);
            return new HandlerResult(string.Empty);
        }

        if (text.Trim()
                .Equals("/start", StringComparison.OrdinalIgnoreCase))
        {
            return new HandlerResult("StartStep");
        }

        await SendInvalid(update);

        return new HandlerResult(string.Empty);
    }

    private async Task SendInvalid(Update update)
    {
        if (update.Message != null)
        {
            var chatId = update.Message.Chat.Id;

            if (chatId != 0)
            {
                var msg = _notification.GenerateNotificationMessage(NotificationType.N1_InvalidCommand)!;
                await _bot.SendMessage(chatId, msg);
            }
        }
    }
}
