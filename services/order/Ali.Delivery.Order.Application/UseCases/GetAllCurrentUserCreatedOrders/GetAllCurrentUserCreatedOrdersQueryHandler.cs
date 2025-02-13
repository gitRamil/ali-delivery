using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Application.UseCases.GetAllCreatedOrders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderStatus = Ali.Delivery.Order.Domain.Entities.Dictionaries.OrderStatus;

namespace Ali.Delivery.Order.Application.UseCases.GetAllCurrentUserCreatedOrders;

/// <summary>
/// Представляет обработчик команды получения всех созданных заказов пользователя.
/// </summary>
public class GetAllCurrentUserCreatedOrdersQueryHandler : IRequestHandler<GetAllCurrentUserCreatedOrdersQuery, List<OrderDto>>
{
    private readonly IAppDbContext _context;
    private readonly ICurrentUser _currentUser;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="GetAllCreatedOrdersQueryHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <param name="currentUser">Текущий пользователь.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public GetAllCurrentUserCreatedOrdersQueryHandler(IAppDbContext context, ICurrentUser currentUser)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    /// <inheritdoc />
    public async Task<List<OrderDto>> Handle(GetAllCurrentUserCreatedOrdersQuery query, CancellationToken cancellationToken)
    {
        var orders = await _context.Orders.Where(o => o.OrderStatus == OrderStatus.Created && o.Sender != null && (Guid)o.Sender.Id == _currentUser.Id)
                                   .Select(order => OrderDto.FromOrder(order))
                                   .ToListAsync(cancellationToken);

        return orders;
    }
}
