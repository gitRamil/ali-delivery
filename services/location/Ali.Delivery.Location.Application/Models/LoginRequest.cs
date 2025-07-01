namespace Ali.Delivery.Location.Application.Models;

/// <summary>
/// Представляет запрос на аутентификацию, содержащий учетные данные пользователя.
/// </summary>
/// <param name="Login">Логин.</param>
/// <param name="Password">Пароль.</param>
public record LoginRequest(string Login, string Password);