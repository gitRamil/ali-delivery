namespace Ali.Delivery.Location.Application.Models;

/// <summary>
/// Определяет набор типизированных уведомлений, которые система может отправить пользователю.
/// </summary>
public enum NotificationType
{
    /// <summary>
    /// Запрос на ввод учетных данных (логина и пароля).
    /// </summary>
    N0_EnterCredentials,

    /// <summary>
    /// Приветственное сообщение для нового пользователя.
    /// </summary>
    N1_Welcome,

    /// <summary>
    /// Сообщение о неверной команде на этапе аутентификации.
    /// </summary>
    N1_InvalidAuthCommand,
    
    /// <summary>
    /// Сообщение о завершении сессии (например, по команде /stop).
    /// </summary>
    N_SessionEnded,

    /// <summary>
    /// Общее сообщение о нераспознанной команде.
    /// </summary>
    N1_InvalidCommand,

    /// <summary>
    /// Сообщение о неверных учетных данных (логин или пароль).
    /// </summary>
    N2_InvalidCredentials,

    /// <summary>
    /// Сообщение об успешном завершении аутентификации.
    /// </summary>
    N4_AuthenticationComplete,

    /// <summary>
    /// Запрос на отправку геолокации.
    /// </summary>
    N5_RequestLocation,

    /// <summary>
    /// Подтверждение того, что геолокация была получена и сохранена.
    /// </summary>
    N6_LocationReceived,

    /// <summary>
    /// Сообщение о том, что отправленная геолокация имеет неверный формат или значение.
    /// </summary>
    N7_InvalidLocation
}
