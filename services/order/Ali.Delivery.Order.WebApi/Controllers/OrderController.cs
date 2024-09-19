using Ali.Delivery.Order.Application.Dtos;
using Ali.Delivery.Order.Application.UseCases.GetOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ali.Delivery.Order.WebApi.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiExplorerSettings(GroupName = "v1")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="OrderController" />.
    /// </summary>
    /// <param name="mediator">Медиатор.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="mediator" /> равен <c>null</c>.
    /// </exception>
    public OrderController(IMediator mediator) => _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    /// <summary>
    /// Получает заказ.
    /// </summary>
    /// <param name="trackNumber">Трек номер заказа.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    [HttpGet("order-info/{trackNumber:guid}")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrder(Guid trackNumber, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetOrderQuery(trackNumber), cancellationToken);
        return Ok(result);
    }
}
