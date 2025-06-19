using Ali.Delivery.Location.Infrastructure.ExternalServices.Models.Configuration;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

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
            case "/geosharing":
                return new HandlerResult("GeoSharing");
            default:
                await SendInvalid(update);
                return new HandlerResult(string.Empty);
        }
    }

    private async Task SendInvalid(Update update)
    {
        var chatId = update.Message?.Chat.Id ?? update.CallbackQuery?.Message?.Chat.Id;

        if (chatId is null)
        {
            return;
        }

        await _bot.SendMessage(chatId.Value, _notification.GenerateNotificationMessage(NotificationType.N1_InvalidCommand));
    }
}
