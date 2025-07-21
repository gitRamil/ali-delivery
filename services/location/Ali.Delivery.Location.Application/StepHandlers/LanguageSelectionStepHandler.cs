using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Constants;
using Ali.Delivery.Location.Application.Models;

namespace Ali.Delivery.Location.Application.StepHandlers;

/// <summary>
/// Обработчик команды выбора языка через кнопки Telegram.
/// </summary>
public class LanguageSelectionStepHandler : IStepHandler
{
    private readonly INotificationService _notification;
    private readonly IUserLanguageService _userLanguageService;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="LanguageSelectionStepHandler" />.
    /// </summary>
    /// <param name="notification">Сервис для отправки уведомлений.</param>
    /// <param name="userLanguageService">Сервис для работы с языковыми настройками пользователей.</param>
    public LanguageSelectionStepHandler(INotificationService notification, IUserLanguageService userLanguageService)
    {
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
        _userLanguageService = userLanguageService ?? throw new ArgumentNullException(nameof(userLanguageService));
    }

    /// <inheritdoc />
    public async Task<HandlerResult> HandleAsync(MessageInfo messageInfo, CancellationToken cancellationToken = default)
    {
        var langCode = messageInfo.Text switch
        {
            "Русский 🇷🇺" => "ru",
            "English 🇬🇧" => "en",
            _ => null
        };

        if (langCode != null)
        {
            await _userLanguageService.SetUserLanguage(messageInfo.ChatId, langCode);
            await _notification.SendNotificationMessageAsync(messageInfo.ChatId, NotificationType.LanguageChanged, cancellationToken: cancellationToken);
            return new HandlerResult(Steps.Start);
        }

        if (string.Equals(messageInfo.Text, Commands.Stop, StringComparison.OrdinalIgnoreCase))
        {
            await _notification.SendNotificationMessageAsync(messageInfo.ChatId, NotificationType.SessionEnded, cancellationToken: cancellationToken);
            return new HandlerResult(Steps.Stop);
        }

        await _notification.SendNotificationMessageAsync(messageInfo.ChatId, NotificationType.LanguagePrompt, cancellationToken: cancellationToken);
        return new HandlerResult(Steps.LanguageSelection);
    }
}
