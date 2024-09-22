using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.CreateOrder;

/// <summary>
/// Представляет обработчик команды создания оценщика для карт целей.
/// </summary>
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="CreateOrderCommandHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// >
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public CreateOrderCommandHandler(IAppDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="command" /> равен <c>null</c>.
    /// </exception>
    public async Task Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        _context.Orders.Add(new Domain.Entities.Order(SequentialGuid.Create(), new OrderName(command.OrderName), OrderStatus.Created));
        await _context.SaveChangesAsync(cancellationToken);
    }
}
