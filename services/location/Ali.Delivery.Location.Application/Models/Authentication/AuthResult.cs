namespace Ali.Delivery.Location.Application.Models.Authentication;

/// <summary>
/// Определяет возможные статусы результата операции аутентификации.
/// </summary>
public enum AuthResult
{
    /// <summary>
    /// Указывает, что аутентификация прошла успешно.
    /// </summary>
    Success,

    /// <summary>
    /// Указывает, что предоставленные учетные данные (логин/пароль) неверны.
    /// </summary>
    InvalidCredentials,

    /// <summary>
    /// Указывает, что пользователь с таким логином не найден и требуется регистрация.
    /// </summary>
    RegistrationRequired,

    /// <summary>
    /// Указывает, что в процессе аутентификации произошла непредвиденная ошибка.
    /// </summary>
    Error
}
