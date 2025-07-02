using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Models;

namespace Ali.Delivery.Location.Infrastructure.Services;

/// <summary>
/// Реализация <see cref="INotificationLocalizationService" />, получающая переводы уведомлений из словаря.
/// </summary>
public class NotificationLocalizationService : INotificationLocalizationService
{
    private readonly Dictionary<string, Dictionary<string, string>> _notifications;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="NotificationLocalizationService" />.
    /// </summary>
    /// <param name="notifications">
    /// Словарь переводов уведомлений: ключ — тип уведомления, значение — словарь языков и
    /// переводов.
    /// </param>
    public NotificationLocalizationService(Dictionary<string, Dictionary<string, string>> notifications) => _notifications = notifications;

    /// <inheritdoc />
    public string GetNotificationMessage(NotificationType type, string languageCode)
    {
        var key = type.ToString();

        if (!_notifications.TryGetValue(key, out var translations))
        {
            return $"[No localization for {key}]";
        }

        if (translations.TryGetValue(languageCode, out var message))
        {
            return message;
        }

        return translations.TryGetValue("en", out var fallback) ? fallback : $"[No localization for {key}]";
    }
}
