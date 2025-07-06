namespace Ali.Delivery.Location.Application.Models;

/// <summary>
/// Представляет базовую информацию о пользователе в системе.
/// </summary>
/// <param name="Login">Логин пользователя. Может быть <c>null</c>, если пользователь не аутентифицирован.</param>
public record UserInfo(string? Login);
