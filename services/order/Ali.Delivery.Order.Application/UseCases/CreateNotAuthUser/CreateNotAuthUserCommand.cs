using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.CreateNotAuthUser;

/// <summary>
/// Представляет команду создания незарегистрированного пользователя.
/// </summary>
/// <param name="NotAuthUserFirstName">Имя незарегистрированного пользователя.</param>
/// <param name="NotAuthUserLastName">Фамилия незарегистрированного пользователя.</param>
/// <param name="NotAuthUserPhoneNumber">Телефонный номер незарегистрированного пользователя.</param>
public record CreateNotAuthUserCommand(string NotAuthUserFirstName, string NotAuthUserLastName, string NotAuthUserPhoneNumber) : IRequest<Guid>;
