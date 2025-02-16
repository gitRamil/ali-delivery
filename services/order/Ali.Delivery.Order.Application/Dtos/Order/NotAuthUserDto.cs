namespace Ali.Delivery.Order.Application.Dtos.Order;

/// <summary>
/// Представляет незарегистрированного пользователя.
/// </summary>
/// <param name="Id">Идентификатор.</param>
/// <param name="FirstName">Имя.</param>
/// <param name="LastName">Фамилия.</param>
/// <param name="PhoneNumber">Номер паспорта.</param> 

public sealed record NotAuthDto(Guid Id, string FirstName, string LastName, string PhoneNumber);
