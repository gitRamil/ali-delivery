using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.GetAllCreatedOrders;

/// <summary>
/// Команда для получения всех созданных заказов.
/// </summary>
public class GetAllCreatedOrdersCommand : IRequest<List<OrderDto>>;