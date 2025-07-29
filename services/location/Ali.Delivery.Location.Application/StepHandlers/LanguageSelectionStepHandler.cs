using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Constants;
using Ali.Delivery.Location.Application.Extensions;
using Ali.Delivery.Location.Application.Models;
using Ali.Delivery.Location.Domain.Entities.Dictionaries;

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
        var chatId = messageInfo.ChatId;

        if (messageInfo is not { Text: { } text })
        {
            await _notification.SendNotificationMessageAsync(chatId, NotificationType.InvalidAuthCommand, cancellationToken: cancellationToken);
            return new HandlerResult(string.Empty);
        }

        var languageDictionary = messageInfo.Text switch
        {
            "Русский 🇷🇺" => LanguageDictionary.Russian,
            "English 🇬🇧" => LanguageDictionary.English,
            _ => null
        };

        if (languageDictionary != null)
        {
            await _userLanguageService.UpsertUserLanguageAsync(chatId, languageDictionary, cancellationToken);
            await _notification.SendNotificationMessageAsync(chatId, NotificationType.LanguageChanged, cancellationToken: cancellationToken);
            await _notification.SendNotificationMessageAsync(chatId, NotificationType.AuthCompleteCommandHelp, cancellationToken: cancellationToken);

            return new HandlerResult(Steps.AuthComplete);
        }

        switch (text.MessageToCommand())
        {
            case Commands.Stop:
                await _notification.SendNotificationMessageAsync(chatId, NotificationType.SessionEnded, cancellationToken: cancellationToken);
                return new HandlerResult(Steps.Stop);
            default:
                await _notification.SendNotificationMessageAsync(chatId, NotificationType.LanguagePrompt, cancellationToken: cancellationToken);
                return new HandlerResult(Steps.LanguageSelection);
        }
    }
}
