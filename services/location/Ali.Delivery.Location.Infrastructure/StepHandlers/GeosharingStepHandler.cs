using Ali.Delivery.Location.Infrastructure.ExternalServices.Models.Configuration;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.StepHandlers;

public class GeosharingStepHandler : IStepHandler
{
    private readonly ITelegramBotClient _bot;
    private readonly ICommandMethods _method;
    private readonly INotificationService _notification;

    public GeosharingStepHandler(ITelegramBotClient bot, ICommandMethods method, INotificationService notification)
    {
        _bot = bot ?? throw new ArgumentNullException(nameof(bot));
        _method = method ?? throw new ArgumentNullException(nameof(method));
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
    }

    public async Task<HandlerResult> HandleAsync(Update update)
    {
        if (update.Message?.Location is { } loc)
        {
            var res = await _method.GeoSharingAsync(update.Message.Chat.Id, $"{loc.Latitude} {loc.Longitude}");

            return new HandlerResult(res.NextStepKey);
        }

        if (update.Message?.Text is not { } text)
        {
            await _bot.SendMessage(update.Message.Chat.Id, _notification.GenerateNotificationMessage(NotificationType.N5_RequestLocation)!);
            return new HandlerResult(string.Empty);
        }

        var command = text.Trim()
                          .ToLowerInvariant();

        return command switch
        {
            "/stop_geosharing" => new HandlerResult("AuthComplete"),
            _ => new HandlerResult(string.Empty)
        };
    }
}
