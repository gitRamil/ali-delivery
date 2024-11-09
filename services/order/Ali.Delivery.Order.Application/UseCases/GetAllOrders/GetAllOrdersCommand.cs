using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.GetAllOrders;

/// <summary>
/// Представляет команду для получения всех заказов.
/// </summary>
public class GetAllOrders : IRequest<List<OrderDto>>
{
}
