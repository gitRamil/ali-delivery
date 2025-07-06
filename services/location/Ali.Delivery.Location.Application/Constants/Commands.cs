namespace Ali.Delivery.Location.Application.Constants;

/// <summary>
/// Содержит строковые константы для всех поддерживаемых команд пользователя в Telegram-боте.
/// </summary>
public static class Commands
{
    /// <summary>
    /// Команда для запуска бота и начала новой сессии.
    /// </summary>
    public const string Start = "/start";

    /// <summary>
    /// Команда для завершения текущей сессии пользователя.
    /// </summary>
    public const string Stop = "/stop";

    /// <summary>
    /// Команда для перехода в режим геошеринга (отправки локации).
    /// </summary>
    public const string Geosharing = "/geosharing";

    /// <summary>
    /// Команда для выхода из режима геошеринга и возврата в состояние ожидания.
    /// </summary>
    public const string StopGeosharing = "/stop_geosharing";

    /// <summary>
    /// Команда для начала процесса аутентификации (ввода логина и пароля).
    /// </summary>
    public const string Login = "/login";
}
