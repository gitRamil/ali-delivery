using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.GetOrder;

/// <summary>
/// Представляет обработчик запроса на получение заказа.
/// </summary>
public class GetOrderCommandHandler : IRequestHandler<GetOrderCommand, OrderDto>
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="GetOrderCommandHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public GetOrderCommandHandler(IAppDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <exception cref="NotFoundException"></exception>
    /// <inheritdoc />
    public async Task<OrderDto> Handle(GetOrderCommand query, CancellationToken cancellationToken)
    {

        var order = await _context.Orders.Select(order =>new OrderDto(order.Id,
                                                 order.Name,
                                                 order.OrderStatus.Name,
                                                 order.OrderInfo.OrderInfoWeight,
                                                 order.OrderInfo.OrderInfoPrice,
                                                 order.OrderInfo.OrderInfoAddressFrom,
                                                 order.OrderInfo.OrderInfoAddressTo))
                                  .FirstOrDefaultAsync(cancellationToken);
        if (order == null)
        {
            throw new NotFoundException(typeof(Domain.Entities.Order), query.OrderId); 
        }
        
                    
        return order;
    }
}
