using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.CreateUser;

/// <summary>
/// Представляет команду создания пользователя.
/// </summary>
/// <param name="FirstName">Имя пользователя.</param>
/// <param name="LastName">Фамилия пользователя.</param>
/// <param name="PassportType">Тип паспорта пользователя</param>
/// <param name="PassportNumber">Номер паспорта.</param>
/// <param name="RegDate">Дата регистрации паспорта.</param>
/// <param name="ExpirationDate">Дата окончания действия паспорта.</param>
/// <param name="Role">Код роли пользователя.</param>
/// <param name="Birthday">Дата рождения пользователя.</param>
public record CreateUserCommand(
    string Login,
    string Password,
    string FirstName,
    string LastName,
    PassportType PassportType,
    string PassportNumber,
    DateTime RegDate,
    DateTime ExpirationDate,
    RoleCode Role,
    DateTime Birthday) : IRequest<Guid>;
