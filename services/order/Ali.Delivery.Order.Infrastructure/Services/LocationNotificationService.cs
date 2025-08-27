using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Hubs;
using Ali.Delivery.Order.Application.Models;
using Microsoft.AspNetCore.SignalR;

namespace Ali.Delivery.Order.Infrastructure.Services;

/// <summary>
/// Сервис для отправки уведомлений о локациях через SignalR.
/// </summary>
public class LocationNotificationService : ILocationNotificationService
{
    private readonly IHubContext<LocationHub> _hubContext;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="LocationNotificationService" />.
    /// </summary>
    /// <param name="hubContext">Контекст Hub'а.</param>
    public LocationNotificationService(IHubContext<LocationHub> hubContext) => _hubContext = hubContext;

    /// <inheritdoc />
    public async Task NotifyLocationCreated(LocationCreatedMessage locationMessage)
    {
        var locationData = new
        {
            id = locationMessage.Id,
            latitude = locationMessage.Latitude,
            longitude = locationMessage.Longitude,
            createdAt = locationMessage.CreatedAt,
            timestamp = DateTime.UtcNow
        };

        await _hubContext.Clients.Group("AllLocations")
                         .SendAsync("LocationUpdated", locationData);
    }

    /// <inheritdoc />
    public async Task NotifyUserLocationCreated(string userId, LocationCreatedMessage locationMessage)
    {
        var locationData = new
        {
            id = locationMessage.Id,
            latitude = locationMessage.Latitude,
            longitude = locationMessage.Longitude,
            createdAt = locationMessage.CreatedAt,
            timestamp = DateTime.UtcNow,
            userId
        };

        await _hubContext.Clients.Group($"User-{userId}")
                         .SendAsync("UserLocationUpdated", locationData);
    }
}
