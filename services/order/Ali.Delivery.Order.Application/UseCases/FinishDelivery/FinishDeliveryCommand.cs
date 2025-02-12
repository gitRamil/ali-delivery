using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.FinishDelivery;

/// <summary>
/// Представляет команду для завершения заказа курьером.
/// </summary>
/// <param name="OrderId">Идентификатор заказа.</param>
public record FinishDeliveryCommand(Guid OrderId) : IRequest<Guid>;
