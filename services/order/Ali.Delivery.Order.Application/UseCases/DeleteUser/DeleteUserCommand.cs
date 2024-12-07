using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.DeleteUser;

/// <summary>
/// Представляет команду для удаления пользователя.
/// </summary>
/// <param name="UserId">Идентификатор пользователя.</param>
public record DeleteUserCommand(Guid UserId) : IRequest;
