using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.CreateUser;

/// <summary>
/// Представляет команду создания пользователя.
/// </summary>
/// <param name="Login">Логин.</param>
/// <param name="Password">Пароль.</param>
/// <param name="Role">Код роли.</param>
/// <param name="Birthday">Дата рождения.</param>
public record CreateUserCommand(string Login, string Password, RoleCode Role, DateTime Birthday) : IRequest<Guid>;
