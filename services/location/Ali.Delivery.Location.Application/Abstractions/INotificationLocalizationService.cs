using Ali.Delivery.Location.Application.Models;

namespace Ali.Delivery.Location.Application.Abstractions;

/// <summary>
/// Сервис локализации уведомлений. Позволяет получать текст уведомления по типу и языку.
/// </summary>
public interface INotificationLocalizationService
{
    /// <summary>
    /// Получает текст уведомления по типу и языковому коду.
    /// </summary>
    /// <param name="type">Тип уведомления (<see cref="NotificationType" />).</param>
    /// <param name="languageCode">Языковой код (например, "ru", "en").</param>
    /// <returns>Локализованный текст уведомления.</returns>
    string GetNotificationMessage(NotificationType type, string languageCode);
}
