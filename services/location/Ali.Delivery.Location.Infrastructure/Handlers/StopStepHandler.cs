using Ali.Delivery.Location.Infrastructure.ExternalServices.Models.Configuration;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Handlers;

public sealed class StopStepHandler : IStepHandler
{
    private readonly ITelegramBotClient _bot;
    private readonly INotificationService _notification;

    public StopStepHandler(ITelegramBotClient bot, INotificationService notification)
    {
        _bot = bot;
        _notification = notification;
    }

    public async Task<HandlerResult> HandleAsync(Update update)
    {
        if (update.Message is not { Text: { } text })
        {
            await SendInvalid(update);

            return new HandlerResult
            {
                NextStepOption = string.Empty
            };
        }

        if (text.Trim()
                .Equals("/start", StringComparison.OrdinalIgnoreCase))
        {
            return new HandlerResult
            {
                NextStepOption = "StartStep"
            };
        }

        await SendInvalid(update);

        return new HandlerResult
        {
            NextStepOption = string.Empty
        };
    }

    public async Task OnEnterAsync(long chatId)
    {
        var msg = _notification.GenerateNotificationMessage(NotificationType.N_SessionEnded)!;
        await _bot.SendMessage(chatId, msg);
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
