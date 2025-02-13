using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.GetOrder;

/// <summary>
/// Представляет запрос на получение заказа.
/// </summary>
/// <param name="OrderId">Идентификатор заказа.</param>
public sealed record GetOrderQuery(Guid OrderId) : IRequest<OrderDto>;
