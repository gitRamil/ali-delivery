using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Models;
using Ali.Delivery.Location.Domain.Entities;
using MediatR;

namespace Ali.Delivery.Location.Application.UseCases.CreateOrUpdateUserLocationCommand;

/// <summary>
/// </summary>
public class CreateOrUpdateUserLocationCommandHandler : IRequestHandler<CreateOrUpdateUserLocationCommand, CommandResult>
{
    private readonly IAppDbContext _context;
    private readonly IUserLocationRepository _repository;

    /// <summary>
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="context"></param>
    public CreateOrUpdateUserLocationCommandHandler(IUserLocationRepository repository, IAppDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    public async Task<CommandResult> Handle(CreateOrUpdateUserLocationCommand request, CancellationToken cancellationToken)
    {
        if (await _repository.IsUserExistsAsync(request.UserLogin, cancellationToken))
        {
            var userLocation = await _repository.GetUserAsync(request.UserLogin, cancellationToken);

            if (userLocation != null)
            {
                userLocation.UpdateCoordinates(request.Latitude, request.Longitude);
                await _repository.UpdateUserLocationAsync(userLocation, cancellationToken);
            }
        }
        else
        {
            var newUserLocation = new UserLocation(SequentialGuid.Create(), request.UserLogin);
            newUserLocation.UpdateCoordinates(request.Latitude, request.Longitude);
            await _repository.AddUserLocationAsync(newUserLocation, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return new CommandResult(string.Empty);
    }
}
