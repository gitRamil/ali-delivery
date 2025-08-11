namespace Ali.Delivery.Location.Application.Models.Authentication;

/// <summary>
/// Представляет результат операции аутентификации.
/// </summary>
/// <param name="AuthResult">Статус результата операции аутентификации.</param>
/// <param name="UserId">Идентификатор пользователя.</param>
public record AuthenticationResult(AuthResult AuthResult, Guid? UserId);
