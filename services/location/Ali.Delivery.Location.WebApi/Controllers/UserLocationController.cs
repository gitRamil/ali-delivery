using Ali.Delivery.Location.Application.UseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ali.Delivery.Location.WebApi.Controllers;

/// <summary>
/// Контроллер для управления действиями курьера.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiExplorerSettings(GroupName = "v1")]
public class UserLocationController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="UserLocationController" />.
    /// </summary>
    /// <param name="mediator">Медиатор.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="mediator" /> равен <c>null</c>.
    /// </exception>
    public UserLocationController(IMediator mediator) => _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    /// <summary>
    /// Назначает курьера на заказ.
    /// </summary>
    /// <param name="orderId">Номер заказа.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    /// <returns>ID заказа.</returns>
    [HttpPut("create-location")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateUserLocation(string UserLogin, string E, string S, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateUserLocationCommand(UserLogin, E, S), cancellationToken);
        return Ok(result);
    }
}
