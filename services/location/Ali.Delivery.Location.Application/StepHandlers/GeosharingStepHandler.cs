using System.Globalization;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Models;
using Ali.Delivery.Location.Domain.Entities;

namespace Ali.Delivery.Location.Application.StepHandlers;

public class GeosharingStepHandler : IStepHandler
{
    private readonly INotificationService _notification;
    private readonly IUserLocationRepository _repository;

    public GeosharingStepHandler(INotificationService notification, IUserLocationRepository repository)
    {
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        
    }

    public async Task<HandlerResult> HandleAsync(MessageInfo messageInfo, CancellationToken cancellationToken)
    {
         if (messageInfo.Location is { } loc)
         {
             return await ProcessLocationAsync(messageInfo.ChatId, loc.Latitude.ToString(CultureInfo.InvariantCulture), loc.Longitude.ToString(CultureInfo.InvariantCulture), cancellationToken);
         }
         
         if (messageInfo.Text is { } text)
         {
             return await ProcessTextCommandAsync(messageInfo.ChatId, text);
         }
         
         await _notification.SendNotificationMessageAsync(messageInfo.ChatId, NotificationType.N5_RequestLocation);
         return new HandlerResult(string.Empty);
    }

    private async Task<HandlerResult> ProcessLocationAsync(long chatId, string latitude, string longitude, CancellationToken cancellationToken)
    {
        var userLogin = chatId.ToString();
        var userLocation = await _repository.GetUserAsync(userLogin, cancellationToken);

        if (userLocation is not null)
        {
            userLocation.UpdateCoordinates(longitude, latitude);
            await _repository.UpdateUserLocationAsync(userLocation, cancellationToken);
        }
        else
        {
            var newUserLocation = new UserLocation(SequentialGuid.Create(), userLogin);
            newUserLocation.UpdateCoordinates(longitude, latitude);
            await _repository.AddUserLocationAsync(newUserLocation, cancellationToken);
        }
        
        await _notification.SendNotificationMessageAsync(chatId, NotificationType.N6_LocationReceived,new Dictionary<string, object>
        {
            ["Latitude"] = latitude,
            ["Longitude"] = longitude
        });
        
        return new HandlerResult(string.Empty);
    }

    private async Task<HandlerResult> ProcessTextCommandAsync(long chatId, string text)
    {
        var command = text.Trim().ToLowerInvariant();

        switch (command)
        {
            case "/stop_geosharing":
                await _notification.SendNotificationMessageAsync(chatId, NotificationType.N4_AuthenticationComplete);
                return new HandlerResult("AuthComplete");
            case "/stop":
                await _notification.SendNotificationMessageAsync(chatId, NotificationType.N_SessionEnded);
                return new HandlerResult("StopStep");
            default:
                await _notification.SendNotificationMessageAsync(chatId, NotificationType.N5_RequestLocation);
                return new HandlerResult(string.Empty);
        }
    }
}
