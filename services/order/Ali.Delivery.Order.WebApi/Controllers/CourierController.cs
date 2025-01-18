using Ali.Delivery.Order.Application;
using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Application.UseCases.AssignCourier;
using Ali.Delivery.Order.Application.UseCases.FinishDelivery;
using Ali.Delivery.Order.Application.UseCases.GetAllCourierFinishedOrders;
using Ali.Delivery.Order.Application.UseCases.GetAllOrdersByUserId;
using Ali.Delivery.Order.Application.UseCases.UnassignCourier;
using Ali.Delivery.Order.WebApi.Attribute;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ali.Delivery.Order.WebApi.Controllers;

/// <summary>
/// Контроллер для управления действиями курьера.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiExplorerSettings(GroupName = "v1")]
public class CourierController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="UserController" />.
    /// </summary>
    /// <param name="mediator">Медиатор.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="mediator" /> равен <c>null</c>.
    /// </exception>
    public CourierController(IMediator mediator) => _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    /// <summary>
    /// Назначает курьера на заказ.
    /// </summary>
    /// <param name="orderId">Номер заказа.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    /// <returns>ID заказа.</returns>
    [HttpPut("assign-courier")]
    [UserPermission(UserPermissionCode.OrderManagement)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AssignCourier(Guid orderId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new AssignCourierCommand(orderId), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Завершение заказа курьером.
    /// </summary>
    /// <param name="orderId">Номер заказа.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    /// <returns>ID заказа</returns>
    [HttpPut("finish-delivery")]
    [UserPermission(UserPermissionCode.OrderManagement)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FinishDelivery(Guid orderId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new FinishDeliveryCommand(orderId), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получает все заказы курьера со статусом завершено.
    /// </summary>
    /// <param name="cancellationToken">Маркер отмены.</param>
    /// <returns>Список заказов</returns>
    [HttpGet("couriers-orders-finished")]
    [UserPermission(UserPermissionCode.OrderManagement)]
    [ProducesResponseType(typeof(List<OrderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllCourierFinishedOrders(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllCourierFinishedOrdersCommand(), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получает все заказы курьера со статусом в работе.
    /// </summary>
    /// <param name="cancellationToken">Маркер отмены.</param>
    /// <returns>Список заказов</returns>
    [HttpGet("couriers-orders-in-progress")]
    [UserPermission(UserPermissionCode.OrderManagement)]
    [ProducesResponseType(typeof(List<OrderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllCourierOrdersInProgress(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllCourierOrdersInProgressCommand(), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Снимает курьера с заказа.
    /// </summary>
    /// <param name="orderId">Номер заказа.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    /// <returns>ID заказа</returns>
    [HttpPut("unassign-courier")]
    [UserPermission(UserPermissionCode.OrderManagement)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UnassignCourier(Guid orderId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UnassignCourierCommand(orderId), cancellationToken);
        return Ok(result);
    }
}
