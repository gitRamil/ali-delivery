using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Application.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderStatus = Ali.Delivery.Order.Domain.Entities.Dictionaries.OrderStatus;

namespace Ali.Delivery.Order.Application.UseCases.GetAllCreatedOrders;

/// <summary>
/// Представляет обработчик запроса на получение списка всех заказов курьера со статусом "created".
/// </summary>
public class GetAllCreatedOrdersCommandHandler : IRequestHandler<GetAllCreatedOrdersCommand, List<OrderDto>>
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="GetAllCreatedOrdersCommandHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public GetAllCreatedOrdersCommandHandler(IAppDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc />
    public async Task<List<OrderDto>> Handle(GetAllCreatedOrdersCommand request, CancellationToken cancellationToken)
    {
        var orders = await _context.Orders
                                   .Where(o=> o.OrderStatus == OrderStatus.Created)
                                   .Select(order => OrderDto.FromOrder(order))
                                   .ToListAsync(cancellationToken);

        return orders;
    }
}
