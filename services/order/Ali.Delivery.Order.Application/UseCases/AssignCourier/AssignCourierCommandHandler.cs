using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Application.Exceptions;
using Ali.Delivery.Order.Application.Extensions;
using Ali.Delivery.Order.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.AssignCourier;

/// <summary>
/// Представляет обработчик команды назначения курьера.
/// </summary>
public class AssignCourierCommandHandler : IRequestHandler<AssignCourierCommand, Guid>
{
    private readonly IAppDbContext _context;
    private readonly ICurrentUser _currentUser;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="AssignCourierCommandHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <param name="currentUser">Текущий пользователь</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public AssignCourierCommandHandler(IAppDbContext context, ICurrentUser currentUser)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="request" /> равен <c>null</c>.
    /// </exception>
    public async Task<Guid> Handle(AssignCourierCommand request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => (Guid)o.Id == request.OrderId, cancellationToken) ??
                    throw new NotFoundException(typeof(Domain.Entities.Order), request.OrderId);

        if (order.OrderStatus.Code == "inProgress" || order.OrderStatus.Code == "finished")
        {
            throw new InvalidOperationException(order.OrderStatus.Code == "inProgress" ? "Заказ уже назначен на другого курьера" : "Заказ уже завершен");
        }

        order.Courier = await _context.Users.FirstOrDefaultAsync(u => (Guid)u.Id == _currentUser.Id, cancellationToken) ??
                        throw new NotFoundException(typeof(User), _currentUser.Id);
        order.OrderStatus = OrderStatus.InProgress.ToOrderStatus();

        await _context.SaveChangesAsync(cancellationToken);
        return order.Id;
    }
}
