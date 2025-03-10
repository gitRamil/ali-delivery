using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Application.Exceptions;
using Ali.Delivery.Order.Application.Extensions;
using Ali.Delivery.Order.Domain.ValueObjects.Order;
using Ali.Delivery.Order.Domain.ValueObjects.OrderInfo;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.UpdateOrder;

/// <summary>
/// Представляет обработчик команды для обновления заказа.
/// </summary>
public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderDto>
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="UpdateOrderCommandHandler" />.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public UpdateOrderCommandHandler(IAppDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="command" /> равен <c>null</c>.
    /// </exception>
    public async Task<OrderDto> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var order = await _context.Orders.FirstOrDefaultAsync(o => (Guid)o.Id == command.OrderId, cancellationToken) ??
                    throw new NotFoundException(typeof(Domain.Entities.Order), command.OrderId);

        order.UpdateOrderName(new OrderName(command.OrderName));

        var orderInfo = order.OrderInfo;

        orderInfo.UpdateOrderInfo(new OrderInfoWeight(command.Weight),
                                  new OrderInfoPrice(command.Price),
                                  new OrderInfoAddressFrom(command.AddressFrom),
                                  new OrderInfoAddressTo(command.AddressTo),
                                  command.Size.ToSize());

        order.UpdateOrderStatus(command.OrderStatus.ToOrderStatus());

        await _context.SaveChangesAsync(cancellationToken);

        return OrderDto.FromOrder(order);
    }
}
