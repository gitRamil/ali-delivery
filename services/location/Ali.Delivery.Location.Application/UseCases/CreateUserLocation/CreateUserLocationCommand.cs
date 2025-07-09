using Ali.Delivery.Location.Domain.Entities;
using MediatR;

namespace Ali.Delivery.Location.Application.UseCases.CreateUserLocation;

/// <summary>
/// Представляет команду создания локации пользователя.
/// </summary>
/// <param name="UserId">Id пользователя.</param>
/// <param name="E">Координаты долготы.</param>
/// <param name="S">Координаты широты.</param>
public record CreateUserLocationCommand(User UserId, string E, string S) : IRequest<Guid>;
