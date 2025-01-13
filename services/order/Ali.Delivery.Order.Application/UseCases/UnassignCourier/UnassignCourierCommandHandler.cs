using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Application.Exceptions;
using Ali.Delivery.Order.Application.Extensions;
using Ali.Delivery.Order.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.UnassignCourier;

/// <summary>
/// Обработчик команды отказа курьера.
/// </summary>
public class UnassignCourierCommandHandler : IRequestHandler<UnassignCourierCommand, Guid>
{
    private readonly IAppDbContext _context;
    private readonly ICurrentUser _currentUser;

    public UnassignCourierCommandHandler(IAppDbContext context, ICurrentUser currentUser)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    public async Task<Guid> Handle(UnassignCourierCommand request, CancellationToken cancellationToken)
    {
        // Проверяем роль текущего пользователя
        if (!_currentUser.HasPermission(UserPermissionCode.OrderManagement))
        {
            throw new UnauthorizedAccessException("Текущий пользователь не является курьером.");
        }

        // Ищем заказ
        var order = await _context.Orders
                                  .FirstOrDefaultAsync(o => (Guid)o.Id == request.OrderId, cancellationToken)
                    ?? throw new NotFoundException(typeof(Domain.Entities.Order), request.OrderId);

        // Проверяем, что текущий пользователь является назначенным курьером
        
        var currentUser = await _context.Users
                                        .FirstOrDefaultAsync(u => (Guid)u.Id == _currentUser.Id, cancellationToken)
                          ?? throw new NotFoundException(typeof(User), _currentUser.Id); 
        
        if (order.Courier != currentUser)
        {
            throw new UnauthorizedAccessException("Текущий пользователь не является назначенным курьером для этого заказа.");
        }

        // Сбрасываем курьера и меняем статус
        order.Courier = null;
        order.OrderStatus = OrderStatus.Created.ToOrderStatus();

        await _context.SaveChangesAsync(cancellationToken);
        return order.Id;
    }
}