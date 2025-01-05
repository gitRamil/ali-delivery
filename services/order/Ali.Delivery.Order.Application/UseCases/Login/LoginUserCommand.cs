using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.Login;

/// <summary>
/// Представляет запрос на вход пользователя в систему.
/// </summary>
/// <param name="Login">Логин пользователя.</param>
/// <param name="Password">Пароль пользователя.</param>
public record LoginCommand(string Login, string Password) : IRequest<string>;
