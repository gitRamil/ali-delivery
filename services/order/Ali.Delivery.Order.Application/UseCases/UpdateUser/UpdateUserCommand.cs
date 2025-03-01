using Ali.Delivery.Order.Application.Dtos.Enums;
using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.UpdateUser;

/// <summary>
/// Представляет команду обновления данных пользователя.
/// </summary>
/// <param name="UserId">Идентификатор.</param>
/// <param name="Login">Логин.</param>
/// <param name="FirstName">Имя.</param>
/// <param name="LastName">Фамилия.</param>
/// <param name="PassportType">Тип паспорта.</param>
/// <param name="PassportNumber">Номер паспорта.</param>
/// <param name="RegDate">Дата регистрации паспорта.</param>
/// <param name="IssuedBy">Кем выдан.</param>
/// <param name="Role">Роль.</param>
/// <param name="Birthdate">Дата рождения.</param>
public record UpdateUserCommand(
    Guid UserId,
    string Login,
    string FirstName,
    string LastName,
    PassportType PassportType,
    string PassportNumber,
    DateTime RegDate,
    string IssuedBy,
    RoleCode Role,
    DateTime Birthdate) : IRequest<UserDto>;
