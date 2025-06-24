using System.Globalization;
using Ali.Delivery.Location.Application.UseCases.CreateOrUpdateUserLocationCommand;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Ali.Delivery.Location.Infrastructure.Models;
using MediatR;
using Telegram.Bot;

namespace Ali.Delivery.Location.Application.StepHandlers;

public class GeosharingStepHandler : IStepHandler
{
    private readonly ITelegramBotClient _bot;
    private readonly INotificationService _notification;
    private readonly IMediator _mediator;

    public GeosharingStepHandler(ITelegramBotClient bot, INotificationService notification, IMediator mediator)
    {
        _bot = bot ?? throw new ArgumentNullException(nameof(bot));
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<HandlerResult> HandleAsync(MessageInfo messageInfo)
    {
        if (messageInfo.Location is { } loc)
        {
            var locationCommand = new CreateOrUpdateUserLocationCommand(
                messageInfo.ChatId.ToString(),
                loc.Longitude.ToString(CultureInfo.InvariantCulture),
                loc.Latitude.ToString(CultureInfo.InvariantCulture)
            );

            // 2. Отправляем команду через MediatR
            var result = await _mediator.Send(locationCommand); // CancellationToken можно передать, если он есть в HandleAsync
            var messageWithLocation = _notification.GenerateNotificationMessage(NotificationType.N6_LocationReceived,
                                                                                new Dictionary<string, object>
                                                                                {
                                                                                    ["Latitude"] = loc.Latitude,
                                                                                    ["Longitude"] = loc.Longitude
                                                                                });
            await _bot.SendMessage(messageInfo.ChatId, messageWithLocation);
            return new HandlerResult(result.NextStepKey);
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
