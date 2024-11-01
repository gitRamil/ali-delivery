using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Extensions;
using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.ValueObjects.Order;
using Ali.Delivery.Order.Domain.ValueObjects.OrderInfo;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.CreateOrder;

/// <summary>
/// Представляет обработчик команды создания заказа.
/// </summary>
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="CreateOrderCommandHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public CreateOrderCommandHandler(IAppDbContext context) =>
        _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <summary>
    /// Выполняет команду создания заказа.
    /// </summary>
    /// <param name="request">Команда.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="request" /> равен <c>null</c>.
    /// </exception>
    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var orderInfo = new OrderInfo(
            SequentialGuid.Create(),
            new Weight(request.Weight),
            request.Size.ToSize(),   
            new Price(request.Price),
            new AddressFrom(request.AddressFrom),
            new AddressTo(request.AddressTo)
        );
        
        var order = new Domain.Entities.Order(
            SequentialGuid.Create(),
            new OrderName(request.OrderName),
            orderInfo,
            request.OrderStatus.ToOrderStatus() 
        );
        
        _context.Orders.Add(order);
        
        await _context.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
}
