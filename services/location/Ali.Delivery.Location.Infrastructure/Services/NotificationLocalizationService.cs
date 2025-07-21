using System.Text.Json;
using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Models;

namespace Ali.Delivery.Location.Infrastructure.Services;

/// <summary>
/// Реализация <see cref="INotificationLocalizationService" />, получающая переводы уведомлений из словаря.
/// </summary>
public class NotificationLocalizationService : INotificationLocalizationService
{
    private const string DefaultLanguage = "ru";
    private readonly Dictionary<string, Dictionary<string, string>> _notifications;
    private readonly IUserLanguageService _userLanguageService;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="NotificationLocalizationService" />.
    /// </summary>
    public NotificationLocalizationService(IUserLanguageService userLanguageService)
    {
        var notificationsJson = File.ReadAllText("notifications.json");

        _notifications = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(notificationsJson) ??
                         throw new InvalidOperationException("Ошибка получения конфигурации с языками");
        _userLanguageService = userLanguageService ?? throw new ArgumentNullException(nameof(userLanguageService));
    }

    /// <inheritdoc />
    public async Task<string> GetNotificationMessage(long chatId, NotificationType type, Dictionary<string, string>? placeholderData = null)
    {
        var notificationTypeKey = type.ToString();

        if (!_notifications.TryGetValue(notificationTypeKey, out var messageWithLanguages))
        {
            throw new InvalidOperationException($"Не найдено сообщение по типу сообщения {notificationTypeKey} в файле конфигурации");
        }

        var languageCode = await _userLanguageService.GetUserLanguageAsync(chatId) ?? DefaultLanguage;

        if (!messageWithLanguages.TryGetValue(languageCode, out var messageByLanguage))
        {
            throw new InvalidOperationException($"Не найдено сообщение с типом {notificationTypeKey} по языку {languageCode} в файле конфигурации");
        }

        var messageWithPlaceholderData = AddPlaceholderData(messageByLanguage, placeholderData);
        return messageWithPlaceholderData;
    }

    private static string AddPlaceholderData(string messageByLanguage, Dictionary<string, string>? placeholderData)
    {
        if (placeholderData == null || placeholderData.Count == 0)
        {
            return messageByLanguage;
        }

        var result = messageByLanguage;

        foreach (var kvp in placeholderData)
        {
            result = result.Replace($"{{{kvp.Key}}}", kvp.Value ?? string.Empty);
        }

        return result;
    }
}
