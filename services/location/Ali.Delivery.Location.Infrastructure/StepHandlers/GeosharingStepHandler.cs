using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;
using Ali.Delivery.Location.Infrastructure.ExternalServices.Models.Configuration;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Telegram.Bot;

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

    public async Task<HandlerResult> HandleAsync(MessageInfo messageInfo)
    {
        if (messageInfo.Location is { } loc)
        {
            var res = await _method.GeoSharingAsync(messageInfo.ChatId, $"{loc.Latitude} {loc.Longitude}");
            return new HandlerResult(res.NextStepKey);
        }

        if (messageInfo.Text is not { } text)
        {
            await _bot.SendMessage(messageInfo.ChatId, _notification.GenerateNotificationMessage(NotificationType.N5_RequestLocation));
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
