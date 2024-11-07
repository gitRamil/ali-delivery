using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.CreateUser
{
    /// <summary>
    /// Команда для создания пользователя.
    /// </summary>
    public record CreateUserCommand(
        string FirstName,
        string LastName,
        PassportType PassportType,
        string PassportNumber,
        DateTime RegDate,
        DateTime ExpirationDate,
        RoleCode Role,
        DateTime Birthday) : IRequest<Guid>;
}
