using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.GetOrder;

/// <summary>
/// Представляет обработчик запроса на получение заказа.
/// </summary>
public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDto>
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="GetOrderQueryHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public GetOrderQueryHandler(IAppDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="query" /> равен <c>null</c>.
    /// </exception>
    public async Task<OrderDto> Handle(GetOrderQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        var order = await _context.Orders.FirstOrDefaultAsync(o => (Guid)o.Id == query.OrderId, cancellationToken) ??
                    throw new NotFoundException(typeof(Domain.Entities.Order), query.OrderId);

        return OrderDto.FromOrder(order);
    }
}
