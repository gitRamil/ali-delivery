using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.GetUser;

/// <summary>
/// Представляет запрос на получение пользователя.
/// </summary>
/// <param name="UserId">Идентификатор заказа.</param>
public sealed record GetUserCommand(Guid UserId) : IRequest<UserDto>;
