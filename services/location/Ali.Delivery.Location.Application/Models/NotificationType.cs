namespace Ali.Delivery.Location.Application.Models;

/// <summary>
/// Определяет набор типизированных уведомлений, которые система может отправить пользователю.
/// </summary>
public enum NotificationType
{
    /// <summary>
    /// Запрос на ввод учетных данных (логина и пароля).
    /// </summary>
    EnterCredentials,

    /// <summary>
    /// Приветственное сообщение для нового пользователя.
    /// </summary>
    Welcome,

    /// <summary>
    /// Сообщение о неверной команде на этапе аутентификации.
    /// </summary>
    InvalidAuthCommand,

    /// <summary>
    /// Сообщение о завершении сессии (например, по команде /stop).
    /// </summary>
    SessionEnded,

    /// <summary>
    /// Общее сообщение о нераспознанной команде.
    /// </summary>
    InvalidCommand,

    /// <summary>
    /// Сообщение о неверных учетных данных (логин или пароль).
    /// </summary>
    InvalidCredentials,

    /// <summary>
    /// Сообщение об успешном завершении аутентификации.
    /// </summary>
    AuthenticationComplete,

    /// <summary>
    /// Запрос на отправку геолокации.
    /// </summary>
    RequestLocation,

    /// <summary>
    /// Подтверждение того, что геолокация была получена и сохранена.
    /// </summary>
    LocationReceived,

    /// <summary>
    /// Сообщение о том, что отправленная геолокация имеет неверный формат или значение.
    /// </summary>
    InvalidLocation,

    /// <summary>
    /// Сообщение об успешной смене языка.
    /// </summary>
    LanguageChanged,

    /// <summary>
    /// Сообщение с просьбой выбрать язык (обычно с клавиатурой).
    /// </summary>
    LanguagePrompt,

    /// <summary>
    /// .
    /// </summary>
    LocationWithPlaceHolder
}
