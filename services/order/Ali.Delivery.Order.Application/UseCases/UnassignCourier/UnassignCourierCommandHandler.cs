using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Application.Exceptions;
using Ali.Delivery.Order.Application.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.UnassignCourier;

/// <summary>
/// Представляет обработчик команды отказа курьера.
/// </summary>
public class UnassignCourierCommandHandler : IRequestHandler<UnassignCourierCommand, Guid>
{
    private readonly IAppDbContext _context;
    private readonly ICurrentUser _currentUser;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="UnassignCourierCommandHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <param name="currentUser">Текущий пользователь</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public UnassignCourierCommandHandler(IAppDbContext context, ICurrentUser currentUser)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="request" /> равен <c>null</c>.
    /// </exception>
    public async Task<Guid> Handle(UnassignCourierCommand request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => (Guid)o.Id == request.OrderId, cancellationToken) ??
                    throw new NotFoundException(typeof(Domain.Entities.Order), request.OrderId);

        if (order.Courier != null && (Guid)order.Courier.Id != _currentUser.Id)
        {
            throw new UnauthorizedAccessException("Текущий пользователь не является назначенным курьером для этого заказа.");
        }

        order.Courier = null;
        order.OrderStatus = OrderStatus.Created.ToOrderStatus();

        await _context.SaveChangesAsync(cancellationToken);
        return order.Id;
    }
}
