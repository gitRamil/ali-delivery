using Ali.Delivery.Location.Domain.Entities;
using MediatR;

namespace Ali.Delivery.Location.Application.UseCases.CreateUserLocation;

/// <summary>
/// Представляет команду создания локации пользователя.
/// </summary>
/// <param name="UserId">Id пользователя.</param>
/// <param name="Longitude">Координаты долготы.</param>
/// <param name="Latitude">Координаты широты.</param>
public record CreateUserLocationCommand(User UserId, double Longitude, double Latitude) : IRequest<Guid>;
