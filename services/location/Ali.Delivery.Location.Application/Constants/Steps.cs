namespace Ali.Delivery.Location.Application.Constants;

/// <summary>
/// Содержит строковые константы для всех идентификаторов шагов (состояний) конечного автомата.
/// </summary>
public static class Steps
{
    /// <summary>
    /// Состояние, в котором пользователь успешно аутентифицирован и может выполнять основные действия.
    /// </summary>
    public const string AuthComplete = "AuthComplete";

    /// <summary>
    /// Состояние, в котором пользователь проходит аутентификацию (вводит логин и пароль).
    /// </summary>
    public const string Authorization = "Authorization";

    /// <summary>
    /// Состояние, в котором бот ожидает и обрабатывает геолокацию пользователя.
    /// </summary>
    public const string GeoSharing = "GeoSharing";

    /// <summary>
    /// Состояние, в котором осуществляется выбор языка.
    /// </summary>
    public const string LanguageSelection = "LanguageSelection";

    /// <summary>
    /// Начальное состояние (шаг) бота, в котором пользователь только начал сессию.
    /// </summary>
    public const string Start = "StartStep";

    /// <summary>
    /// Финальное состояние, в котором сессия пользователя завершена.
    /// </summary>
    public const string Stop = "StopStep";
}
