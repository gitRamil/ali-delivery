using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Exceptions;
using Ali.Delivery.Location.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Location.Application.UseCases.UpdateUserLocation;

/// <summary>
/// Представляет обработчик команды для обновления локации пользователя.
/// </summary>
public class UpdateUserLocationCommandHandler : IRequestHandler<UpdateUserLocationCommand, string>
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="UpdateUserLocationCommandHandler" />.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public UpdateUserLocationCommandHandler(IAppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="command" /> равен <c>null</c>.
    /// </exception>
    public async Task<string> Handle(UpdateUserLocationCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var userLocation =
            await _context.UserLocations.FirstOrDefaultAsync(u => u.TelegramLogin == command.UserLogin,
                cancellationToken) ??
            throw new NotFoundException(typeof(UserLocation), command.UserLogin);

        userLocation.UpdateCoordinates(command.E, command.S);
        await _context.SaveChangesAsync(cancellationToken);

        return $"E: {userLocation.E}, S: {userLocation.S}";
    }
}