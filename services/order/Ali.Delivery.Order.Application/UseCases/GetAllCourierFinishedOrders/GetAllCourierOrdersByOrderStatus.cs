using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.GetAllCourierFinishedOrders;

/// <summary>
/// Представляет команду для удаления пользователя.
/// </summary>
public record GetAllCourierOrdersByOrderStatus(OrderStatus OrderStatus) : IRequest<List<OrderDto>>;
