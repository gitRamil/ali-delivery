using Ali.Delivery.Location.Application.Models;

namespace Ali.Delivery.Location.Application.Interfaces;

public interface INotificationService
{
    Task SendNotificationMessageAsync(long chatId, NotificationType notification, Dictionary<string, object>? userData = null);
}
