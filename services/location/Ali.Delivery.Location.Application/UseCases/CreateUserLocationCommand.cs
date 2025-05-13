using MediatR;

namespace Ali.Delivery.Location.Application.UseCases;

public record CreateUserLocationCommand(string UserLogin, string E, string S) : IRequest<string>;

