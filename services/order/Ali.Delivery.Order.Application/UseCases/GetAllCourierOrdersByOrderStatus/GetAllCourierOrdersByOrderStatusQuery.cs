using Ali.Delivery.Order.Application.Dtos.Enums;
using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.GetAllCourierOrdersByOrderStatus;

/// <summary>
/// Представляет команду для получения заказов курьера по статусу.
/// </summary>
/// <param name="OrderStatusCode">Статус заказа.</param>
public record GetAllCourierOrdersByOrderStatusQuery(OrderStatusCode OrderStatusCode) : IRequest<List<OrderDto>>;
