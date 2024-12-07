using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.DeleteOrder;

/// <summary>
/// Представляет команду для удаления заказа.
/// </summary>
/// <param name="OrderId">Идентификатор заказа.</param>
public record DeleteOrderCommand(Guid OrderId): IRequest;
