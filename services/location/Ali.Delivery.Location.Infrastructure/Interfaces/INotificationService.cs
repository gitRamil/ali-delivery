using Ali.Delivery.Location.Infrastructure.ExternalServices.Models.Configuration;

namespace Ali.Delivery.Location.Infrastructure.Interfaces;

public interface INotificationService
{
    string? GenerateNotificationMessage(NotificationType? notification, Dictionary<string, object>? userData = null);
}
