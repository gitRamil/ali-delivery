using Ali.Delivery.Location.Application.Models;

namespace Ali.Delivery.Location.Application.Interfaces;

public interface INotificationService
{
    string GenerateNotificationMessage(NotificationType? notification, Dictionary<string, object>? userData = null);
}
