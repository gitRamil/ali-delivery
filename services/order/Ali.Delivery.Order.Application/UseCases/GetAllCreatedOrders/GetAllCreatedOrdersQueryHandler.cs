using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderStatus = Ali.Delivery.Order.Domain.Entities.Dictionaries.OrderStatus;

namespace Ali.Delivery.Order.Application.UseCases.GetAllCreatedOrders;

/// <summary>
/// Представляет обработчик запроса на получение списка всех активных заказов.
/// </summary>
public class GetAllCreatedOrdersQueryHandler : IRequestHandler<GetAllCreatedOrdersQuery, List<OrderDto>>
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="GetAllCreatedOrdersQueryHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public GetAllCreatedOrdersQueryHandler(IAppDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc />
    public async Task<List<OrderDto>> Handle(GetAllCreatedOrdersQuery query, CancellationToken cancellationToken)
    {
        var orders = await _context.Orders.Where(o => o.OrderStatus == OrderStatus.Created)
                                   .Select(order => OrderDto.FromOrder(order))
                                   .ToListAsync(cancellationToken);

        return orders;
    }
}
