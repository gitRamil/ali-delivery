using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.GetAllUsers;

/// <summary>
/// Представляет команду для получения всех пользователей.
/// </summary>
public record GetAllUsers : IRequest<List<UserDto>>;
