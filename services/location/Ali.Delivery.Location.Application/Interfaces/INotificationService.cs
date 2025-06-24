using Ali.Delivery.Location.Infrastructure.Models;

namespace Ali.Delivery.Location.Application.Interfaces;

public interface INotificationService
{
    string GenerateNotificationMessage(NotificationType? notification, Dictionary<string, object>? userData = null);
}
