using Ali.Delivery.Domain.Core.Primitives;
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
    public async Task<OrderDto> Handle(GetOrderCommand request, CancellationToken cancellationToken)
    {
        return await _context.Orders.Where(o => o.Id == (SequentialGuid)request.OrderId)
                             .Select(o => new OrderDto(o.Id,
                                                       o.Name,
                                                       o.OrderStatus.Name,
                                                       o.OrderInfo.OrderInfoWeight,
                                                       o.OrderInfo.OrderInfoPrice,
                                                       o.OrderInfo.OrderInfoAddressFrom,
                                                       o.OrderInfo.OrderInfoAddressTo))
                             .FirstOrDefaultAsync(cancellationToken) ??
               throw new NotFoundException(typeof(Domain.Entities.Order), request.OrderId);
    }
}
