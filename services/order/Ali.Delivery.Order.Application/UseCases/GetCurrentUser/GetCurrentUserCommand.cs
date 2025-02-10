using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.GetCurrentUser;

/// <summary>
/// Представляет запрос на получение текущего пользователя.
/// </summary>
public sealed record GetCurrentUserCommand : IRequest<UserDto>;
