using System.Globalization;
using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace Ali.Delivery.Location.Infrastructure.Services;

public class NotificationService(ILogger<NotificationService> logger) : INotificationService
{
    private static readonly Dictionary<NotificationType, string> NotificationMessages = new()
    {
        [NotificationType.N0_EnterCredentials] = "Введите логин и пароль (например, user123 pass).",
        [NotificationType.N1_Welcome] = "Добро пожаловать! Для начала работы введите /login.",
        [NotificationType.N1_InvalidAuthCommand] = "Неизвестная команда. Пожалуйста, введите /login для аутентификации.",
        [NotificationType.N_SessionEnded] = "Сессия завершена. Введите /start для новой сессии.",
        [NotificationType.N1_InvalidCommand] = "Неизвестная команда или действие для текущего состояния. Пожалуйста, используйте доступные команды.",
        [NotificationType.N2_InvalidCredentials] = "Неверные учетные данные. Пожалуйста, введите логин и пароль повторно.",
        [NotificationType.N3_RegistrationRequired] = "Требуется регистрация. Пожалуйста, зарегистрируйтесь или обратитесь к администратору.",
        [NotificationType.N4_AuthenticationComplete] = "Авторизация успешно завершена. Используйте /geosharing для начала отслеживания или /stop для выхода.",
        [NotificationType.N5_RequestLocation] = "Пожалуйста, поделитесь вашей геопозицией для продолжения или введите /stop для отмены.",
        [NotificationType.N6_LocationReceived] = "Ваша локация получена. Продолжайте делиться или введите /stop.",
        [NotificationType.N7_InvalidLocation] = "Не удалось сохранить локацию. Пожалуйста, попробуйте снова или введите /stop."
    };

    private readonly ILogger<NotificationService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public string? GenerateNotificationMessage(NotificationType? notification, Dictionary<string, object>? userData = null)
    {
        if (notification == null)
        {
            return null;
        }

        var message = GetBaseNotificationMessage(notification.Value); // Используем .Value

        if (notification == NotificationType.N6_LocationReceived && userData != null)
        {
            message = EnrichLocationMessage(message, userData);
        }

        _logger.LogInformation("Generated notification message for type {NotificationType}: '{Message}'", notification, message);
        return message;
    }

    private string EnrichLocationMessage(string baseMessage, Dictionary<string, object> userData)
    {
        if (!userData.TryGetValue("Latitude", out var latObj) || !userData.TryGetValue("Longitude", out var lonObj))
        {
            return baseMessage;
        }

        try
        {
            var latitude = Convert.ToDouble(latObj, CultureInfo.InvariantCulture)
                                  .ToString("F4", CultureInfo.InvariantCulture);

            var longitude = Convert.ToDouble(lonObj, CultureInfo.InvariantCulture)
                                   .ToString("F4", CultureInfo.InvariantCulture);
            return $"Локация получена: Широта {latitude}, Долгота {longitude}. Продолжайте делиться или введите /stop.";
        }
        catch (FormatException ex)
        {
            _logger.LogError(ex, "Error formatting latitude/longitude for notification. Raw Latitude: '{LatObj}', Raw Longitude: '{LonObj}'", latObj, lonObj);
        }

        return baseMessage;
    }

    private string GetBaseNotificationMessage(NotificationType notification) =>
        NotificationMessages.TryGetValue(notification, out var message) ? message : $"Неизвестный тип уведомления ({notification}). Обратитесь к разработчику.";
}
