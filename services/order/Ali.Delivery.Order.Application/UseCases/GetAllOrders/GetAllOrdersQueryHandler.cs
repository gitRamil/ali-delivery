using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.GetAllOrders;

/// <summary>
/// Представляет обработчик запроса на получение списка всех заказов.
/// </summary>
public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrders, List<OrderDto>>
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="GetAllOrdersQueryHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public GetAllOrdersQueryHandler(IAppDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc />
    public async Task<List<OrderDto>> Handle(GetAllOrders query, CancellationToken cancellationToken)
    {
        var orders = await _context.Orders.Select(order => OrderDto.FromOrder(order))
                                   .ToListAsync(cancellationToken);
        return orders;
    }
}
