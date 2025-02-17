using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.GetAllBasicUserOrdersByOrderStatus;

/// <summary>
/// Представляет команду для получения заказов базового пользователя по статусу.
/// </summary>
/// <param name="OrderStatus">Статус заказа.</param>
public record GetAllBasicUserOrdersByOrderStatusQuery(OrderStatus OrderStatus) : IRequest<List<OrderDto>>;
