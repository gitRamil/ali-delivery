using Ali.Delivery.Location.Application.UseCases.CreateUserLocation;
using Ali.Delivery.Location.Application.UseCases.UpdateUserLocation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ali.Delivery.Location.WebApi.Controllers;

/// <summary>
/// Контроллер для управления действиями с базой локаций.
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
    public UserLocationController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Создает локацию пользователя.
    /// </summary>
    /// <param name="userLogin">Логин пользователя.</param>
    /// <param name="s">Координаты S.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    /// <param name="e">Координаты E.</param>
    /// <returns>Статус команды.</returns>
    [HttpPost("create-location")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateUserLocation(string userLogin, string e, string s,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateUserLocationCommand(userLogin, e, s), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Обновляет локацию пользователя.
    /// </summary>
    /// <param name="userLogin">Логин пользователя.</param>
    /// <param name="s">Координаты S.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    /// <param name="e">Координаты E.</param>
    /// <returns>Статус команды.</returns>
    [HttpPut("update-location")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUserLocation(string userLogin, string e, string s,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UpdateUserLocationCommand(userLogin, e, s), cancellationToken);
        return Ok(result);
    }
}