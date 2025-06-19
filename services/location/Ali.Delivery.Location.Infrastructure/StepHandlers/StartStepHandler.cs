using Ali.Delivery.Location.Infrastructure.ExternalServices.Models.Configuration;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

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

    public async Task<HandlerResult> HandleAsync(Update update)
    {
        if (update.Message is not { Text: { } text })
        {
            await SendInvalid(update);
            return new HandlerResult(string.Empty);
        }

        text = text.Trim()
                   .ToLowerInvariant();

        switch (text)
        {
            case "/start":
                await _bot.SendMessage(update.Message.Chat.Id, _notification.GenerateNotificationMessage(NotificationType.N1_Welcome)!);
                return new HandlerResult(string.Empty);
            case "/login":
                await _bot.SendMessage(update.Message.Chat.Id, _notification.GenerateNotificationMessage(NotificationType.N0_EnterCredentials)!);
                return new HandlerResult("Authorization");
            default:
                await SendInvalid(update);
                return new HandlerResult(string.Empty);
        }
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
