using Ali.Delivery.Order.Application.Dtos.Enums;
using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.GetAllCourierOrdersByOrderStatus;

/// <summary>
/// Представляет команду для получения заказов курьера по статусу.
/// </summary>
/// <param name="OrderStatus">Статус заказа.</param>
public record GetAllCourierOrdersByOrderStatusQuery(OrderStatus OrderStatus) : IRequest<List<OrderDto>>;
