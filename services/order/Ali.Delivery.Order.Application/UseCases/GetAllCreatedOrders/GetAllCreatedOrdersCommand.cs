using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.GetAllCreatedOrders;

public class GetAllCreatedOrdersCommand : IRequest<List<OrderDto>>;