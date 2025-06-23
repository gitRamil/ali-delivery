using System.Globalization;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Ali.Delivery.Location.Infrastructure.Models;
using Ali.Delivery.Location.Infrastructure.Services;
using Telegram.Bot;

namespace Ali.Delivery.Location.Infrastructure.StepHandlers;

public class GeosharingStepHandler : IStepHandler
{
    private readonly ITelegramBotClient _bot;
    private readonly INotificationService _notification;
    private readonly SaveLocationService _saveLocationService;

    public GeosharingStepHandler(ITelegramBotClient bot, INotificationService notification, SaveLocationService saveLocationService)
    {
        _bot = bot ?? throw new ArgumentNullException(nameof(bot));
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
        _saveLocationService = saveLocationService;
    }

    public async Task<HandlerResult> HandleAsync(MessageInfo messageInfo)
    {
        if (messageInfo.Location is { } loc)
        {
            var res = await _saveLocationService.CreateOrUpdateUserLocationAsync(messageInfo.ChatId.ToString(),
                                                                                 loc.Latitude.ToString(CultureInfo.InvariantCulture),
                                                                                 loc.Longitude.ToString(CultureInfo.InvariantCulture));

            var messageWithLocation = _notification.GenerateNotificationMessage(NotificationType.N6_LocationReceived,
                                                                                new Dictionary<string, object>
                                                                                {
                                                                                    ["Latitude"] = loc.Latitude,
                                                                                    ["Longitude"] = loc.Longitude
                                                                                });
            await _bot.SendMessage(messageInfo.ChatId, messageWithLocation);
            return new HandlerResult(res.NextStepKey);
        }

        if (messageInfo.Text is not { } text)
        {
            await _bot.SendMessage(messageInfo.ChatId, _notification.GenerateNotificationMessage(NotificationType.N5_RequestLocation));
            return new HandlerResult(string.Empty);
        }

        var command = text.Trim()
                          .ToLowerInvariant();

        switch (command)
        {
            case "/stop_geosharing":
                await _bot.SendMessage(messageInfo.ChatId, _notification.GenerateNotificationMessage(NotificationType.N4_AuthenticationComplete));
                return new HandlerResult("AuthComplete");
            case "/stop":
                await _bot.SendMessage(messageInfo.ChatId, _notification.GenerateNotificationMessage(NotificationType.N_SessionEnded));
                return new HandlerResult("StopStep");
            default:
                return new HandlerResult(string.Empty);
        }
    }
}
