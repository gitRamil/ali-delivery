using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.GetAllOrdersByUserId;

/// <summary>
/// Команда получения всех заказов курьера "в работе".
/// </summary>
public class GetAllCourierOrdersInProgressCommand : IRequest<List<OrderDto>>;

