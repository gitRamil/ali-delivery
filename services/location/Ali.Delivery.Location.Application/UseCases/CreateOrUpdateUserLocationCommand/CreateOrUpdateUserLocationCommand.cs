using Ali.Delivery.Location.Application.Models;
using MediatR;

namespace Ali.Delivery.Location.Application.UseCases.CreateOrUpdateUserLocationCommand;

/// <summary>
/// </summary>
/// <param name="UserLogin"></param>
/// <param name="Latitude"></param>
/// <param name="Longitude"></param>
public record CreateOrUpdateUserLocationCommand(string UserLogin, string Latitude, string Longitude) : IRequest<CommandResult>;
