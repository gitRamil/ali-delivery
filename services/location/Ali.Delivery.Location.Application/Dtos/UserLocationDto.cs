namespace Ali.Delivery.Location.Application.Dtos;

/// <summary>
/// Представляет локацию пользователя.
/// </summary>
/// <param name="UserLogin">Логин пользователя.</param>
/// <param name="E">Координаты E.</param>
/// <param name="S">Координаты S</param>
public sealed record UserLocationDto(
    string UserLogin,
    string E,
    string S);