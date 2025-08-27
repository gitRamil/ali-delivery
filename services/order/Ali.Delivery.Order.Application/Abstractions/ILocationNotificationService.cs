using Ali.Delivery.Order.Application.Models;

namespace Ali.Delivery.Order.Application.Abstractions;

/// <summary>
/// Интерфейс для отправки уведомлений о локациях через SignalR.
/// </summary>
public interface ILocationNotificationService
{
    /// <summary>
    /// Отправляет уведомление о новой локации всем подключенным клиентам.
    /// </summary>
    Task NotifyLocationCreated(LocationCreatedMessage locationMessage);

    /// <summary>
    /// Отправляет уведомление о локации конкретному пользователю.
    /// </summary>
    Task NotifyUserLocationCreated(string userId, LocationCreatedMessage locationMessage);
}
