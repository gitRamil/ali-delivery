using Ali.Delivery.Order.Application.Dtos.Enums;
using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.GetAllBasicUserOrdersByOrderStatus;

/// <summary>
/// Представляет команду для получения заказов базового пользователя по статусу.
/// </summary>
/// <param name="OrderStatusCode">Статус заказа.</param>
public record GetAllBasicUserOrdersByOrderStatusQuery(OrderStatusCode OrderStatusCode) : IRequest<List<OrderDto>>;
