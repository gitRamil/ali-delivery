using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Application.UseCases.GetAllCreatedOrders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.GetAllCurrentUserOrders;

/// <summary>
/// Представляет обработчик команды получения всех созданных заказов пользователя.
/// </summary>
public class GetAllCurrentUserOrdersCommandHandler : IRequestHandler<GetAllCurrentUserOrdersCommand, List<OrderDto>>
{
    private readonly IAppDbContext _context;
    private readonly ICurrentUser _currentUser;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="GetAllCreatedOrdersCommandHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <param name="currentUser">Текущий пользователь.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public GetAllCurrentUserOrdersCommandHandler(IAppDbContext context, ICurrentUser currentUser)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    /// <inheritdoc />
    public async Task<List<OrderDto>> Handle(GetAllCurrentUserOrdersCommand request, CancellationToken cancellationToken)
    {
        var orders = await _context.Orders.Include(o => o.OrderStatus)
                                   .Include(o => o.OrderInfo)
                                   .Where(o => o.OrderStatus.Code == "created" && o.Sender != null && (Guid)o.Sender.Id == _currentUser.Id)
                                   .Select(order => OrderDto.FromOrder(order))
                                   .ToListAsync(cancellationToken);

        return orders;
    }
}
