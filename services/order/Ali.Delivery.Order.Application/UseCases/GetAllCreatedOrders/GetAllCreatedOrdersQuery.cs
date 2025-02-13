using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.GetAllCreatedOrders;

/// <summary>
/// Команда для получения всех активных заказов.
/// </summary>
public class GetAllCreatedOrdersQuery : IRequest<List<OrderDto>>;
