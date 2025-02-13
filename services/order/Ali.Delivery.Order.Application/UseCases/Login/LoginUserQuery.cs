using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.Login;

/// <summary>
/// Представляет запрос на вход пользователя в систему.
/// </summary>
/// <param name="Login">Логин.</param>
/// <param name="Password">Пароль.</param>
public record LoginUserQuery(string Login, string Password) : IRequest<string>;
