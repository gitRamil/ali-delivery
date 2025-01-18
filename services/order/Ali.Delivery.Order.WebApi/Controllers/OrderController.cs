using Ali.Delivery.Order.Application;
using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Application.UseCases.CreateOrder;
using Ali.Delivery.Order.Application.UseCases.DeleteOrder;
using Ali.Delivery.Order.Application.UseCases.GetAllCreatedOrders;
using Ali.Delivery.Order.Application.UseCases.GetAllCurrentUserOrders;
using Ali.Delivery.Order.Application.UseCases.GetAllOrders;
using Ali.Delivery.Order.Application.UseCases.GetOrder;
using Ali.Delivery.Order.Application.UseCases.UpdateOrder;
using Ali.Delivery.Order.WebApi.Attribute;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ali.Delivery.Order.WebApi.Controllers;

/// <summary>
/// Контроллер для управления действиями с заказами.
/// </summary>
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
    /// Создает заказ.
    /// </summary>
    /// <param name="command">Заказ.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    [HttpPost]
    [UserPermission(UserPermissionCode.OrderManagement)]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Удаляет заказ.
    /// </summary>
    /// <param name="orderId">Идентификатор заказа.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    [HttpDelete]
    [UserPermission(UserPermissionCode.OrderManagement)]
    [ProducesResponseType(typeof(List<OrderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOrder(Guid orderId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteOrderCommand(orderId), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получает список всех созданных заказов.
    /// </summary>
    /// <param name="cancellationToken">Маркер отмены.</param>
    /// <returns>Список всех заказов.</returns>
    [HttpGet("created-orders")]
    [UserPermission(UserPermissionCode.OrderManagement)]
    [ProducesResponseType(typeof(List<OrderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllCreatedOrders(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllCreatedOrdersCommand(), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получает список всех созданных заказов текущего пользователя.
    /// </summary>
    /// <param name="cancellationToken">Маркер отмены.</param>
    /// <returns>Список всех заказов.</returns>
    [HttpGet("currentUser-created-orders")]
    [UserPermission(UserPermissionCode.OrderManagement)]
    [ProducesResponseType(typeof(List<OrderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllCurrentUserCreatedOrders(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllCurrentUserOrdersCommand(), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получает список всех заказов.
    /// </summary>
    /// <param name="cancellationToken">Маркер отмены.</param>
    /// <returns>Список всех заказов.</returns>
    [HttpGet]
    [UserPermission(UserPermissionCode.OrderManagement)]
    [ProducesResponseType(typeof(List<OrderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllOrders(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllOrders(), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получает заказ.
    /// </summary>
    /// <param name="orderId">Идентификатор заказа.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    [HttpGet("{orderId}")]
    [UserPermission(UserPermissionCode.OrderManagement)]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrder(Guid orderId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetOrderCommand(orderId), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Обновляет заказ.
    /// </summary>
    /// <param name="command">Команда обновления заказа.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    [HttpPut]
    [UserPermission(UserPermissionCode.OrderManagement)]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}
