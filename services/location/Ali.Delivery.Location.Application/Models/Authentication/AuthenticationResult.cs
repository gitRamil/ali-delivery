namespace Ali.Delivery.Location.Application.Models.Authentication;

/// <summary>
/// Представляет результат операции аутентификации.
/// </summary>
public class AuthenticationResult
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="AuthenticationResult" />.
    /// </summary>
    /// <param name="status">Статус результата аутентификации.</param>
    /// <param name="userId">Уникальный идентификатор пользователя. По умолчанию <see cref="Guid.Empty" />.</param>
    public AuthenticationResult(AuthResult status, Guid userId = default)
    {
        Status = status;
        UserId = userId;
    }

    /// <summary>
    /// Получает статус результата аутентификации.
    /// </summary>
    /// <value>Значение перечисления <see cref="AuthResult" />, указывающее на успех или причину неудачи аутентификации.</value>
    public AuthResult Status { get; }

    /// <summary>
    /// Получает уникальный идентификатор пользователя.
    /// </summary>
    /// <value>GUID пользователя или <see cref="Guid.Empty" /> если пользователь не аутентифицирован.</value>
    public Guid UserId { get; }
}
