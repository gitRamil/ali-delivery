using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.Login;

/// <summary>
/// </summary>
/// <param name="FirstName"></param>
public record LoginCommand(string FirstName) : IRequest<string>;
