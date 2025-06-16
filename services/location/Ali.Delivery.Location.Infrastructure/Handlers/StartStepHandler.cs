using Ali.Delivery.Location.Infrastructure.ExternalServices.Models.Configuration;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Handlers;

public class StartStepHandler : IStepHandler
{
    private readonly ITelegramBotClient _bot;
    private readonly INotificationService _notification;

    public StartStepHandler(ITelegramBotClient bot, INotificationService notification)
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
                NextStepOption = ""
            };
        }

        text = text.Trim()
                   .ToLowerInvariant();

        switch (text)
        {
            case "/start":
                await OnEnterAsync(update.Message.Chat.Id);

                return new HandlerResult
                {
                    NextStepOption = ""
                };
            case "/login":
                return new HandlerResult
                {
                    NextStepOption = "Authorization"
                };
            default:
                await SendInvalid(update);

                return new HandlerResult
                {
                    NextStepOption = ""
                };
        }
    }

    public async Task OnEnterAsync(long chatId)
    {
        await _bot.SendMessage(chatId, _notification.GenerateNotificationMessage(NotificationType.N1_Welcome)!);
    }

    private async Task SendInvalid(Update update)
    {
        var chatId = update.Message?.Chat.Id;

        if (chatId is null)
        {
            return;
        }

        await _bot.SendMessage(chatId.Value, _notification.GenerateNotificationMessage(NotificationType.N1_InvalidAuthCommand)!);
    }
}
