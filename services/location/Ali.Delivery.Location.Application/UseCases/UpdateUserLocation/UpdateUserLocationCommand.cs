using Ali.Delivery.Location.Domain.Entities;
using MediatR;

namespace Ali.Delivery.Location.Application.UseCases.UpdateUserLocation;

/// <summary>
/// Представляет команду обновления локации пользователя.
/// </summary>
/// <param name="UserId">Id пользователя.</param>
/// <param name="E">Координаты долготы.</param>
/// <param name="S">Координаты широты.</param>
public record UpdateUserLocationCommand(User UserId, string E, string S) : IRequest<string>;
