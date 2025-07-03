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
    /// <param name="chatId">Языковой код (например, "ru", "en").</param>
    /// <param name="type">Тип уведомления (<see cref="NotificationType" />).</param>
    /// <param name="placeholderData"></param>
    /// <returns>Локализованный текст уведомления.</returns>
    Task<string> GetNotificationMessage(long chatId, NotificationType type, Dictionary<string, string>? placeholderData = null);
}
