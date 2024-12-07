using Ali.Delivery.Order.Application.UseCases.RolePermission;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ali.Delivery.Order.WebApi.Controllers;

/// <summary>
/// Контроллер для управления правами ролей.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiExplorerSettings(GroupName = "v1")]
public class RolePermissionsController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Инициализирует новый экземпляр контроллера <see cref="RolePermissionsController" />.
    /// </summary>
    /// <param name="mediator">Медиатор для обработки команд.</param>
    public RolePermissionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Создает связь между ролью и разрешением.
    /// </summary>
    /// <param name="command">Данные для создания связи.</param>
    [HttpPost("create")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateRolePermissionCommand command)
    {
        var rolePermissionId = await _mediator.Send(command);
        return Ok(new { Id = rolePermissionId });
    }
}
