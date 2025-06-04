using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;

namespace Ali.Delivery.Location.Infrastructure.Interfaces2._0;

public interface INotificationService
{
    string? GenerateNotificationMessage(NotificationType? notification, Dictionary<string, object>? userData = null);
}
