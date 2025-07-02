using System.Globalization;
using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Models;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Ali.Delivery.Location.Infrastructure.Services;

/// <summary>
/// Сервис для отправки уведомлений пользователям через Telegram с поддержкой локализации и выбора языка.
/// </summary>
public class NotificationService : INotificationService
{
    private readonly INotificationLocalizationService _localization;
    private readonly ILogger<NotificationService> _logger;
    private readonly ITelegramBotClient _telegramBotClient;
    private readonly IUserStateService _userStateService;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="NotificationService" />.
    /// </summary>
    /// <param name="localization">Сервис локализации уведомлений.</param>
    /// <param name="userStateService">Сервис для получения языка пользователя.</param>
    /// <param name="bot">Клиент для взаимодействия с Telegram Bot API.</param>
    /// <param name="logger">Логгер для записи информации и ошибок.</param>
    public NotificationService(INotificationLocalizationService localization, IUserStateService userStateService, ITelegramBotClient bot, ILogger<NotificationService> logger)
    {
        _localization = localization ?? throw new ArgumentNullException(nameof(localization));
        _userStateService = userStateService ?? throw new ArgumentNullException(nameof(userStateService));
        _telegramBotClient = bot ?? throw new ArgumentNullException(nameof(bot));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc />
    public async Task SendNotificationMessageAsync(long chatId,
                                                   NotificationType notification,
                                                   Dictionary<string, object>? userData = null,
                                                   CancellationToken cancellationToken = default)
    {
        var language = await _userStateService.GetUserLanguageAsync(chatId) ?? "ru";
        var message = _localization.GetNotificationMessage(notification, language);

        if (notification == NotificationType.N9_LanguagePrompt)
        {
            var replyMarkup = new ReplyKeyboardMarkup([
                ["Русский 🇷🇺", "English 🇬🇧"]
            ])
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };

            await _telegramBotClient.SendMessage(chatId, message, replyMarkup: replyMarkup, cancellationToken: cancellationToken);
            return;
        }

        if (notification == NotificationType.N6_LocationReceived && userData != null)
        {
            message = EnrichLocationMessage(message, userData, language);
        }

        _logger.LogInformation("Generated notification message for type {NotificationType}, lang {Lang}: '{Message}'", notification, language, message);

        await _telegramBotClient.SendMessage(chatId, message, cancellationToken: cancellationToken);
    }

    private string EnrichLocationMessage(string baseMessage, Dictionary<string, object> userData, string language)
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

            return language switch
            {
                "en" =>
                    $"Location received: Latitude {latitude}, Longitude {longitude}. Continue sharing or enter /stop_geosharing to wait for a command, or /stop to end the session.",
                _ =>
                    $"Локация получена: Широта {latitude}, Долгота {longitude}. Продолжайте делиться вашей локацией или введите /stop_geosharing для перехода в режим ожидания команды, либо завершите сессию командой /stop."
            };
        }
        catch (FormatException ex)
        {
            _logger.LogError(ex, "Error formatting latitude/longitude for notification. Raw Latitude: '{LatObj}', Raw Longitude: '{LonObj}'", latObj, lonObj);
        }

        return baseMessage;
    }
}
