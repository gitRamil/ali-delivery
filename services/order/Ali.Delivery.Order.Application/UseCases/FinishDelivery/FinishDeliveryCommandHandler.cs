using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Exceptions;
using Ali.Delivery.Order.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.FinishDelivery;

/// <summary>
/// Представляет обработчик команды завершения заказа курьером.
/// </summary>
public class FinishDeliveryCommandHandler : IRequestHandler<FinishDeliveryCommand, Guid>
{
    private readonly IAppDbContext _context;
    private readonly ICurrentUser _currentUser;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="FinishDeliveryCommandHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <param name="currentUser">Текущий пользователь</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public FinishDeliveryCommandHandler(IAppDbContext context, ICurrentUser currentUser)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="command" /> равен <c>null</c>.
    /// </exception>
    public async Task<Guid> Handle(FinishDeliveryCommand command, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => (Guid)o.Id == command.OrderId, cancellationToken) ??
                    throw new NotFoundException(typeof(Domain.Entities.Order), command.OrderId);

        var currentUser = await _context.Users.FirstOrDefaultAsync(u => (Guid)u.Id == _currentUser.Id, cancellationToken) ??
                          throw new NotFoundException(typeof(User), _currentUser.Id);

        order.FinishDelivery(currentUser);

        await _context.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
}
