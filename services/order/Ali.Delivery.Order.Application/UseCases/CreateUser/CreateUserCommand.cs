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
        PassportTypeCodeEnum PassportType,
        string PassportNumber,
        DateTime RegDate,
        DateTime ExpirationDate,
        RoleCodeEnum Role,
        DateTime Birthday
    ) : IRequest<Guid>;
}
