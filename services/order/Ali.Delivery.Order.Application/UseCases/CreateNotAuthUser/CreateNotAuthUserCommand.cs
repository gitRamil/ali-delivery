using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.CreateNotAuthUser;

/// <summary>
/// Представляет команду создания незарегистрированного пользователя.
/// </summary>
/// <param name="FirstName">Имя.</param>
/// <param name="LastName">Фамилия.</param>
/// <param name="PhoneNumber">Телефонный номер.</param>
public record CreateNotAuthUserCommand(string FirstName, string LastName, string PhoneNumber) : IRequest<Guid>;
