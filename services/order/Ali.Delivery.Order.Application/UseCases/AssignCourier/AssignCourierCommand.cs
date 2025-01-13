using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.AssignCourier;

/// <summary>
/// Команда для назначения курьера на заказ.
/// </summary>
public record AssignCourierCommand(Guid OrderId) : IRequest<Guid>;
