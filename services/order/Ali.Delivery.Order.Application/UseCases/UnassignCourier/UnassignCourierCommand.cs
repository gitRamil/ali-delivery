using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.UnassignCourier;

/// <summary>
/// Команда для отказа курьера от заказа.
/// </summary>
/// <param name="OrderId">Номер заказа.</param>
public record UnassignCourierCommand(Guid OrderId) : IRequest<Guid>;
