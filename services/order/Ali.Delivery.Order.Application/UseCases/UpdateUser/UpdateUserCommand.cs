using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.UpdateUser;

/// <summary>
/// Представляет команду обновления данных пользователя.
/// </summary>
/// <param name="UserId">Идентификатор пользователя.</param>
/// <param name="FirstName">Имя пользователя.</param>
/// <param name="LastName">Фамилия пользователя.</param>
/// <param name="PassportType">Тип паспорта пользователя.</param>
/// <param name="PassportNumber">Номер паспорта.</param>
/// <param name="RegDate">Дата регистрации паспорта.</param>
/// <param name="IssuedBy">Кем выдан.</param>
/// <param name="Role">Идентификатор роли пользователя.</param>
/// <param name="Birthdate">Дата рождения пользователя.</param>
public record UpdateUserCommand(
    Guid UserId,
    string FirstName,
    string LastName,
    PassportType PassportType,
    string PassportNumber,
    DateTime RegDate,
    string IssuedBy,
    RoleCode Role,
    DateTime Birthdate) : IRequest<UserDto>;
