using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.CreateOrder;

/// <summary>
/// Команда для создания нового заказа.
/// </summary>
public record CreateOrderCommand(string OrderName, decimal Weight, SizeCode Size, decimal Price, string AddressFrom, string AddressTo, OrderStatus OrderStatus) : IRequest<Guid>;
