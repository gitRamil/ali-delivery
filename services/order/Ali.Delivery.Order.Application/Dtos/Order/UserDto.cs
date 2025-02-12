namespace Ali.Delivery.Order.Application.Dtos.Order;

/// <summary>
/// Представляет пользователя.
/// </summary>
/// <param name="Id">Идентификатор.</param>
/// <param name="FirstName">Имя.</param>
/// <param name="Login">Логин.</param>
/// <param name="LastName">Фамилия.</param>
/// <param name="PassportInfoPassportNumber">Номер паспорта.</param>
/// <param name="PassportType">Тип паспорта.</param>
/// <param name="Role">Роль.</param>
/// <param name="RegDate">Дата регистрации..</param>
/// <param name="IssuedBy">Кем выдан.</param>
public sealed record UserDto(Guid Id, string Login, string? FirstName, string? LastName, string PassportInfoPassportNumber, string? PassportType, string? Role, DateTime? RegDate, string? IssuedBy);
