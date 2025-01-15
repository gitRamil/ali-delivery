using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.FinishDelivery;

/// <summary>
/// Представляет команду для завершения заказа курьером.
/// </summary>
/// <param name="OrderId"></param>
public record  FinishDeliveryCommand(Guid OrderId) : IRequest<Guid>;

