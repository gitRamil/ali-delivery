using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos;
using Ali.Delivery.Order.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.GetOrder;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDto>
{
    private readonly IAppDbContext _context;

    public GetOrderQueryHandler(IAppDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <summary>Handles a request</summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async Task<OrderDto> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        _context.Orders.Add(new Domain.Entities.Order(new SequentialGuid()));
        await _context.SaveChangesAsync(cancellationToken);

        var order = await _context.Orders.FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(typeof(Domain.Entities.Order), request);

        return new OrderDto();
    }
}
