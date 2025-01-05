using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.Login;

/// <summary>
/// </summary>
/// <param name="FirstName"></param>
/// <param name="Password"></param>
public record LoginCommand(string Login, string Password) : IRequest<string>;
