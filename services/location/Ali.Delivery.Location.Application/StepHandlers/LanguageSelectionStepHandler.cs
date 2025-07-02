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
    private readonly IUserStateService _userStateService;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="LanguageSelectionStepHandler" />.
    /// </summary>
    /// <param name="userStateService">Сервис для работы с состоянием пользователя.</param>
    /// <param name="notification">Сервис для отправки уведомлений.</param>
    public LanguageSelectionStepHandler(IUserStateService userStateService, INotificationService notification)
    {
        _userStateService = userStateService;
        _notification = notification;
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
            await _userStateService.SetUserLanguageAsync(messageInfo.ChatId, langCode);
            await _notification.SendNotificationMessageAsync(messageInfo.ChatId, NotificationType.N8_LanguageChanged, cancellationToken: cancellationToken);
            return new HandlerResult(Steps.Start);
        }

        if (string.Equals(messageInfo.Text, Commands.Stop, StringComparison.OrdinalIgnoreCase))
        {
            await _notification.SendNotificationMessageAsync(messageInfo.ChatId, NotificationType.N_SessionEnded, cancellationToken: cancellationToken);
            return new HandlerResult(Steps.Stop);
        }

        await _notification.SendNotificationMessageAsync(messageInfo.ChatId, NotificationType.N9_LanguagePrompt, cancellationToken: cancellationToken);
        return new HandlerResult(Steps.LanguageSelection);
    }
}
