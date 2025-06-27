using System.Globalization;
using Ali.Delivery.Location.Application.Interfaces;
using Ali.Delivery.Location.Application.Models;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace Ali.Delivery.Location.Infrastructure.Services;

public class NotificationService : INotificationService // TODO: Подумать над уведомлениями.
{
    private static readonly Dictionary<NotificationType, string> NotificationMessages = new()
    {
        [NotificationType.N0_EnterCredentials] = "Введите логин и пароль (например, user123 pass).",
        [NotificationType.N1_Welcome] = "Добро пожаловать! Для начала работы введите /login.",
        [NotificationType.N1_InvalidAuthCommand] = "Неизвестная команда. Пожалуйста, введите /login для аутентификации.",
        [NotificationType.N_SessionEnded] = "Сессия завершена. Введите /start для новой сессии.",
        [NotificationType.N1_InvalidCommand] = "Неизвестная команда или действие для текущего состояния. Пожалуйста, используйте доступные команды.",
        [NotificationType.N2_InvalidCredentials] =
            "Неверный логин или пароль, либо вы не зарегистрированы в системе. Пожалуйста пройдите по ссылке и зарегистрируйтесь: https://example.com\n" +
            "Вы можете попробовать еще раз или остановить бота командой /stop.",
        [NotificationType.N4_AuthenticationComplete] = "Авторизация успешно завершена. Используйте /geosharing для начала отслеживания или /stop для выхода.",
        [NotificationType.N5_RequestLocation] =
            "Пожалуйста, поделитесь вашей геопозицией для продолжения или введите /stop_geosharing для перехода в режим ожидания команды, либо завершите сессию командой /stop.",
        [NotificationType.N6_LocationReceived] =
            "Ваша локация получена. Продолжайте делиться своей локацией или введите /stop_geosharing для перехода в режим ожидания команды, либо завершите сессию командой /stop.",
        [NotificationType.N7_InvalidLocation] =
            "Не удалось сохранить локацию. Пожалуйста, попробуйте снова или введите /stop_geosharing для перехода в режим ожидания команды, либо завершите сессию командой /stop."
    };

    private readonly ILogger<NotificationService> _logger;
    private readonly ITelegramBotClient _telegramBotClient;

    public NotificationService(ITelegramBotClient bot, ILogger<NotificationService> logger)
    {
        _telegramBotClient = bot ?? throw new ArgumentNullException(nameof(bot));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task SendNotificationMessageAsync(long chatId, NotificationType notification, Dictionary<string, object>? userData = null)
    {
        var message = GetBaseNotificationMessage(notification);

        if (notification == NotificationType.N6_LocationReceived && userData != null)
        {
            message = EnrichLocationMessage(message, userData);
        }

        _logger.LogInformation("Generated notification message for type {NotificationType}: '{Message}'", notification, message);

        await _telegramBotClient.SendMessage(chatId, message);
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

            return
                $"Локация получена: Широта {latitude}, Долгота {longitude}. Продолжайте делиться вашей локацией или введите /stop_geosharing для перехода в режим ожидания команды, либо завершите сессию командой /stop.";
        }
        catch (FormatException ex)
        {
            _logger.LogError(ex, "Error formatting latitude/longitude for notification. Raw Latitude: '{LatObj}', Raw Longitude: '{LonObj}'", latObj, lonObj);
        }

        return baseMessage;
    }

    private static string GetBaseNotificationMessage(NotificationType notification) =>
        NotificationMessages.TryGetValue(notification, out var message) ? message : $"Неизвестный тип уведомления ({notification}). Обратитесь к разработчику.";
}
