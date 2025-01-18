using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Application.UseCases.GetAllOrdersByUserId;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.GetAllCourierFinishedOrders;

/// <summary>
/// Представляет обработчик запроса на получение списка всех заказов курьера со статусом "finished".
/// </summary>
public class GetAllCourierFinishedOrdersCommandHandler : IRequestHandler<GetAllCourierFinishedOrdersCommand, List<OrderDto>>
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
    public GetAllCourierFinishedOrdersCommandHandler(IAppDbContext context, ICurrentUser currentUser)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    /// <inheritdoc />
    public async Task<List<OrderDto>> Handle(GetAllCourierFinishedOrdersCommand request, CancellationToken cancellationToken)
    {
       
        var orders = await _context.Orders
            .Include(o => o.OrderStatus)
            .Include(o => o.OrderInfo)
            .Where(o=>o.Courier != null && (Guid)o.Courier.Id == _currentUser.Id && o.OrderStatus.Code == "finished" )
            .Select(order => new OrderDto(
                order.Id,
                order.Name,
                order.OrderStatus.Name,
                order.OrderInfo.OrderInfoPrice,
                order.OrderInfo.OrderInfoWeight,
                order.OrderInfo.OrderInfoAddressFrom,
                order.OrderInfo.OrderInfoAddressTo))
            
            .ToListAsync(cancellationToken);

        return orders;
    }
}
