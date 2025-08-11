using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.UseCases.CreateUserLocation;
using Ali.Delivery.Location.Infrastructure.Services;
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
    private readonly IPublisherService _publisherService;
    private readonly IMediator _mediator;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="UserLocationController" />.
    /// </summary>
    /// <param name="mediator">Медиатор.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="mediator" /> равен <c>null</c>.
    /// </exception>
    public UserLocationController(IMediator mediator, IPublisherService publisherService)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _publisherService = publisherService;
    }

    /// <summary>
    /// Создает локацию пользователя.
    /// </summary>
    /// <param name="command">Команда создания локации пользователя.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    /// <returns>Статус команды.</returns>
    [HttpPost("create-location")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateUserLocation([FromBody] CreateUserLocationCommand command, CancellationToken cancellationToken)
    {
        var userId = Guid.NewGuid();
        
        var userCreatedMessage = new UserCreatedMessage
        {
            UserId = userId,
            Email = "QWEQWEQWE",
            Name = "ASDASD",
            CreatedAt = DateTime.UtcNow
        };

        await _publisherService.PublishAsync(userCreatedMessage);

        return Ok(new { UserId = userId });
        // var result = await _mediator.Send(command, cancellationToken);
        // return Ok(result);
    }
}
