using Microsoft.AspNetCore.SignalR;

namespace Ali.Delivery.Order.Application.Hubs;

/// <summary>
/// SignalR Hub для отслеживания локаций в реальном времени.
/// </summary>
public class LocationHub : Hub
{
    /// <summary>
    /// Подписка клиента на обновления всех локаций.
    /// </summary>
    public async Task SubscribeToAllLocations()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "AllLocations");
        await Clients.Caller.SendAsync("SubscribedToAllLocations");
    }

    /// <summary>
    /// Подписка на обновления локаций конкретного пользователя.
    /// </summary>
    public async Task SubscribeToUserLocations(string userId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"User-{userId}");
        await Clients.Caller.SendAsync("SubscribedToUserLocations", userId);
    }

    /// <summary>
    /// Отписка от обновлений всех локаций.
    /// </summary>
    public async Task UnsubscribeFromAllLocations()
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "AllLocations");
        await Clients.Caller.SendAsync("UnsubscribedFromAllLocations");
    }
}
