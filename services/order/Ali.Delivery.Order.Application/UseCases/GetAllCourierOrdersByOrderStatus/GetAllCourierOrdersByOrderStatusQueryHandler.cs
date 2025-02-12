using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Application.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.GetAllCourierOrdersByOrderStatus;

/// <summary>
/// Представляет обработчик запроса на получение списка всех заказов по статусу.
/// </summary>
public class GetAllCourierOrdersByOrderStatusQueryHandler : IRequestHandler<GetAllCourierOrdersByOrderStatusQuery, List<OrderDto>>
{
    private readonly IAppDbContext _context;
    private readonly ICurrentUser _currentUser;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="GetAllCourierOrdersByOrderStatusQueryHandler " />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <param name="currentUser">Текущий пользователь.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> или <paramref name="currentUser" /> равен <c>null</c>.
    /// </exception>
    public GetAllCourierOrdersByOrderStatusQueryHandler(IAppDbContext context, ICurrentUser currentUser)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    /// <inheritdoc />
    public async Task<List<OrderDto>> Handle(GetAllCourierOrdersByOrderStatusQuery query, CancellationToken cancellationToken)
    {
        var orders = await _context.Orders.Where(o => o.OrderStatus == query.OrderStatus.ToOrderStatus() && (Guid)o.Courier!.Id == _currentUser.Id)
                                   .Select(order => OrderDto.FromOrder(order))
                                   .ToListAsync(cancellationToken);

        return orders;
    }
}
