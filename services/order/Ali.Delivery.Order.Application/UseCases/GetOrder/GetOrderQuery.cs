using MediatR;
using Ali.Delivery.Order.Application.Dtos;

namespace Ali.Delivery.Order.Application.UseCases.GetOrder;

public class GetOrderQuery(Guid TrackNumber) : IRequest<OrderDto>;
