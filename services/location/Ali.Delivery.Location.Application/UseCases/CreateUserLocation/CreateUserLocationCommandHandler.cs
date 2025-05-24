using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Domain.Entities;
using MediatR;

namespace Ali.Delivery.Location.Application.UseCases.CreateUserLocation;

/// <summary>
/// Представляет обработчик команды создания локации пользователя.
/// </summary>
public class CreateUserLocationCommandHandler : IRequestHandler<CreateUserLocationCommand, string>
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="CreateUserLocationCommandHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public CreateUserLocationCommandHandler(IAppDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="command" /> равен <c>null</c>.
    /// </exception>
    public async Task<string> Handle(CreateUserLocationCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var userLocation = new UserLocation(SequentialGuid.Create(), command.UserLogin);

        userLocation.UpdateCoordinates(command.E, command.S);

        _context.UserLocations.Add(userLocation);

        await _context.SaveChangesAsync(cancellationToken);

        return userLocation.TelegramLogin;
    }
}
