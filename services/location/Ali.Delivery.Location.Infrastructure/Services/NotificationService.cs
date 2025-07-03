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

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="NotificationService" />.
    /// </summary>
    /// <param name="localization">Сервис локализации уведомлений.</param>
    /// <param name="bot">Клиент для взаимодействия с Telegram Bot API.</param>
    /// <param name="logger">Логгер для записи информации и ошибок.</param>
    public NotificationService(INotificationLocalizationService localization, ITelegramBotClient bot, ILogger<NotificationService> logger)
    {
        _localization = localization ?? throw new ArgumentNullException(nameof(localization));
        _telegramBotClient = bot ?? throw new ArgumentNullException(nameof(bot));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc />
    public async Task SendNotificationMessageAsync(long chatId,
                                                   NotificationType notification,
                                                   Dictionary<string, object>? userData = null,
                                                   CancellationToken cancellationToken = default)
    {
        if (notification == NotificationType.N6_LocationReceived)
        {
            await SendEnrichLocationMessageAsync(chatId, userData, cancellationToken);
            return;
        }

        var message = await _localization.GetNotificationMessage(chatId, notification);

        if (notification == NotificationType.N9_LanguagePrompt)
        {
            await SendChooseLanguageMessageAsync(chatId, message, cancellationToken);
            return;
        }

        await _telegramBotClient.SendMessage(chatId, message, cancellationToken: cancellationToken);
    }

    private async Task SendChooseLanguageMessageAsync(long chatId, string message, CancellationToken cancellationToken)
    {
        var replyMarkup = new ReplyKeyboardMarkup([
            ["Русский 🇷🇺", "English 🇬🇧"]
        ])
        {
            ResizeKeyboard = true,
            OneTimeKeyboard = true
        };

        await _telegramBotClient.SendMessage(chatId, message, replyMarkup: replyMarkup, cancellationToken: cancellationToken);
    }

    private async Task SendEnrichLocationMessageAsync(long chatId, Dictionary<string, object>? userData, CancellationToken cancellationToken)
    {
        if (userData == null)
        {
            _logger.LogError("В сообщении с chatId {ChatId} отсутвует локация пользователя", chatId);
            return;
        }

        if (!userData.TryGetValue("Latitude", out var latObj) || !userData.TryGetValue("Longitude", out var lonObj))
        {
            _logger.LogError("В сообщении с chatId {ChatId} невозможно распарстиь локацию пользователя", chatId);
            return;
        }

        try
        {
            var latitude = Convert.ToDouble(latObj, CultureInfo.InvariantCulture)
                                  .ToString("F4", CultureInfo.InvariantCulture);

            var longitude = Convert.ToDouble(lonObj, CultureInfo.InvariantCulture)
                                   .ToString("F4", CultureInfo.InvariantCulture);

            var locationPlaceholderData = new Dictionary<string, string>
            {
                {
                    "latitude", latitude
                },
                {
                    "longitude", longitude
                }
            };

            var message = await _localization.GetNotificationMessage(chatId, NotificationType.LocationWithPlaceHolder, locationPlaceholderData);

            await _telegramBotClient.SendMessage(chatId, message, cancellationToken: cancellationToken);
        }
        catch (FormatException ex)
        {
            _logger.LogError(ex, "Ошибка получения локации пользователя. Latitude: '{LatObj}', Longitude: '{LonObj}'", latObj, lonObj);
        }
    }
}
