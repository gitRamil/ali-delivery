using MediatR;

namespace Ali.Delivery.Location.Application.UseCases.UpdateUserLocation;

/// <summary>
/// Представляет команду обновления локации пользователя.
/// </summary>
/// <param name="UserLogin">Логин пользователя.</param>
/// <param name="E">Координаты E.</param>
/// <param name="S">Координаты S</param>
public record UpdateUserLocationCommand(string UserLogin, string E, string S) : IRequest<string>;
