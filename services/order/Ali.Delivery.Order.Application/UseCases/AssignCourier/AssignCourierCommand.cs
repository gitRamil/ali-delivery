using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.AssignCourier;

/// <summary>
/// Команда для назначения курьера на заказ.
/// </summary>
/// <param name="OrderId">Номер заказа.</param>
public record AssignCourierCommand(Guid OrderId) : IRequest<Guid>;
