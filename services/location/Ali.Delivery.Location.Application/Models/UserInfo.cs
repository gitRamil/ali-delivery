namespace Ali.Delivery.Location.Application.Models;

/// <summary>
/// Представляет базовую информацию о пользователе в системе.
/// </summary>
/// <param name="Id">Уникальный идентификатор пользователя в системе.</param>
/// <param name="Login">
/// Логин пользователя. Может быть <c>null</c>, если пользователь не аутентифицирован или логин не
/// установлен.
/// </param>
public record UserInfo(Guid Id, string Login);
