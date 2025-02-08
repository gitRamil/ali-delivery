using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.GetAllCurrentUserCreatedOrders;

/// <summary>
/// Представляет команду получения всех созданных заказов пользователя.
/// </summary>
public record GetAllCurrentUserCreatedOrdersCommand : IRequest<List<OrderDto>>;
