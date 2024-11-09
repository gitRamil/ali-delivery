namespace Ali.Delivery.Order.Application.Dtos.Order;

/// <summary>
/// Представляет пользователя.
/// </summary>
/// <param name="Id">Идентификационный номер пользователя.</param>
/// <param name="FirstName">Имя пользователя.</param>
/// <param name="LastName">Фамилия пользователя.</param>
public sealed record UserDto(Guid Id, string FirstName, string LastName);
