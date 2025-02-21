using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.GetUser;

/// <summary>
/// Представляет запрос на получение пользователя.
/// </summary>
/// <param name="UserId">Идентификатор пользователя.</param>
public sealed record GetUserQuery(Guid UserId) : IRequest<UserDto>;
