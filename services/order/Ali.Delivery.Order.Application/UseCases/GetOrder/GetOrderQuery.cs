using Ali.Delivery.Order.Application.Dtos;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.GetOrder;

public class GetOrderQuery(Guid TrackNumber) : IRequest<OrderDto>;
