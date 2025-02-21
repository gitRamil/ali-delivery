using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.DeleteOrder;

/// <summary>
/// Представляет обработчик команды для удаления заказа.
/// </summary>
public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Guid>
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
    /// Возникает, если <paramref name="command" /> равен <c>null</c>.
    /// </exception>
    public async Task<Guid> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var order = await _context.Orders.FirstOrDefaultAsync(o => (Guid)o.Id == command.OrderId, cancellationToken) ??
                    throw new NotFoundException(typeof(Domain.Entities.Order), command.OrderId);

        _context.Orders.Remove(order);

        await _context.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
}
