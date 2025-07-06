namespace Ali.Delivery.Location.Application.Models.Authentication;

/// <summary>
/// Представляет результат операции аутентификации.
/// </summary>
/// <param name="Status">Статус результата аутентификации.</param>
public record AuthenticationResult(AuthResult Status);
