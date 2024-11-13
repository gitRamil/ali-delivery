using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.DeleteOrder;

/// <summary>
/// Представляет обработчик команды для удаления заказа.
/// </summary>
public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, OrderDto>
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="DeleteOrderCommandHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public DeleteOrderCommandHandler(IAppDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="query" /> равен <c>null</c>.
    /// </exception>
    public async Task<OrderDto> Handle(DeleteOrderCommand query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        var order = await _context.Orders.Include(o => o.OrderStatus)
                                  .Include(o => o.OrderInfo)
                                  .FirstOrDefaultAsync(o => (Guid)o.Id == query.OrderId, cancellationToken) ??
                    throw new NotFoundException(typeof(Domain.Entities.Order), query.OrderId);

        var orderDto = new OrderDto(order.Id,
                                    order.Name,
                                    order.OrderStatus.Name,
                                    order.OrderInfo.OrderInfoWeight,
                                    order.OrderInfo.OrderInfoPrice,
                                    order.OrderInfo.OrderInfoAddressFrom,
                                    order.OrderInfo.OrderInfoAddressTo);

        _context.Orders.Remove(order);

        await _context.SaveChangesAsync(cancellationToken);

        return orderDto;
    }
}
