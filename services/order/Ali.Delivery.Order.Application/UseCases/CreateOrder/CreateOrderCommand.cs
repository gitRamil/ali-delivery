using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.CreateOrder;

/// <summary>
/// /// Представляет команду создания заказа.
/// </summary>
/// <param name="OrderName">Наименование заказа.</param>
/// <param name="OrderStatusCode">Код статуса заказа.</param>
public record CreateOrderCommand(string OrderName, OrderStatusCode OrderStatusCode) : IRequest;
