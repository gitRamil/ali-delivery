using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderStatus = Ali.Delivery.Order.Domain.Entities.Dictionaries.OrderStatus;

namespace Ali.Delivery.Order.Application.UseCases.GetAllOrdersByUserId;

/// <summary>
/// Представляет обработчик запроса на получение списка всех заказов курьера со статусом "inprogress".
/// </summary>
public class GetAllCourierOrdersInProgressCommandHandler : IRequestHandler<GetAllCourierOrdersInProgressCommand, List<OrderDto>>
{
    private readonly IAppDbContext _context;
    private readonly ICurrentUser _currentUser;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="GetAllCourierOrdersInProgressCommandHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <param name="currentUser">Текущий пользователь.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> или <paramref name="currentUser" /> равен <c>null</c>.
    /// </exception>
    public GetAllCourierOrdersInProgressCommandHandler(IAppDbContext context, ICurrentUser currentUser)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    /// <inheritdoc />
    public async Task<List<OrderDto>> Handle(GetAllCourierOrdersInProgressCommand request, CancellationToken cancellationToken)
    {
        var courierId = _currentUser.Id;

        var orders = await _context.Orders.Where(o => o.OrderStatus == OrderStatus.InProgress)
                                   .Select(order => OrderDto.FromOrder(order))
                                   .ToListAsync(cancellationToken);

        return orders;
    }
}
