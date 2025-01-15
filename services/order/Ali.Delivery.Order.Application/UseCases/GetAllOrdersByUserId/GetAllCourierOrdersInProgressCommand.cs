using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.GetAllOrdersByUserId;

public class GetAllCourierOrdersInProgressCommand : IRequest<List<OrderDto>>;

