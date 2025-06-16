using Ali.Delivery.Location.Infrastructure.ExternalServices.Models.Configuration;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Handlers;

public class GeosharingStepHandler : IStepHandler
{
    private readonly ITelegramBotClient _bot;
    private readonly ICommandMethods _method;
    private readonly INotificationService _notification;

    public GeosharingStepHandler(ITelegramBotClient bot, INotificationService notification, ICommandMethods method)
    {
        _bot = bot;
        _notification = notification;
        _method = method;
    }

    public async Task<HandlerResult> HandleAsync(Update update)
    {
        if (update.Message?.Location is { } loc)
        {
            var res = await _method.GeoSharingAsync(update.Message.Chat.Id, $"{loc.Latitude} {loc.Longitude}");

            return new HandlerResult
            {
                NextStepOption = res.NextStepKey
            };
        }

        if (update.Message?.Text is not { } text)
        {
            return new HandlerResult
            {
                NextStepOption = ""
            };
        }

        var command = text.Trim()
                          .ToLowerInvariant();

        return command switch
        {
            "/stop_geosharing" => new HandlerResult
            {
                NextStepOption = "AuthComplete"
            },

            _ => new HandlerResult
            {
                NextStepOption = ""
            }
        };
    }

    public async Task OnEnterAsync(long chatId)
    {
        await _bot.SendMessage(chatId, _notification.GenerateNotificationMessage(NotificationType.N5_RequestLocation)!);
    }
}
