using Ali.Delivery.Order.Application;
using Ali.Delivery.Order.Application.Dtos.Enums;
using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Application.UseCases.GetDictionary;
using Ali.Delivery.Order.WebApi.Attribute;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ali.Delivery.Order.WebApi.Controllers;

/// <summary>
/// Контроллер для управления словарями.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiExplorerSettings(GroupName = "v1")]
public class DictionaryController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="DictionaryController" />.
    /// </summary>
    /// <param name="mediator">Медиатор.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="mediator" /> равен <c>null</c>.
    /// </exception>
    public DictionaryController(IMediator mediator) => _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    /// <summary>
    /// Получения объектов словаря.
    /// </summary>
    /// <param name="dictionaryCode"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet("get-dictionary")]
    [UserPermission(UserPermissionCode.UserManagement)]
    [ProducesResponseType(typeof(IReadOnlyCollection<DictionaryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetDictionaryValuesAsync(DictionaryCode dictionaryCode, CancellationToken token)
    {
        var dictionaryValues = await _mediator.Send(new GetDictionaryQuery(dictionaryCode), token);
        return Ok(dictionaryValues);
    }
}
