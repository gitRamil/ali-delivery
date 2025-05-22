using MediatR;

namespace Ali.Delivery.Location.Application.UseCases.CreateUserLocation;

/// <summary>
/// Представляет команду создания локации пользователя.
/// </summary>
/// <param name="UserLogin">Логин пользователя.</param>
/// <param name="E">Координаты E.</param>
/// <param name="S">Координаты S</param>
public record CreateUserLocationCommand(string UserLogin, string E, string S) : IRequest<string>;