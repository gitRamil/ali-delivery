using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Application.Exceptions;
using Ali.Delivery.Order.Application.Extensions;
using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.ValueObjects.PassportInfo;
using Ali.Delivery.Order.Domain.ValueObjects.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.UpdateUser;

/// <summary>
/// Представляет обработчик команды для обновления пользователя.
/// </summary>
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="UpdateUserCommandHandler" />.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public UpdateUserCommandHandler(IAppDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="command" /> равен <c>null</c>.
    /// </exception>
    public async Task<UserDto> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var user = await _context.Users.Include(u => u.Role)
                                 .Include(u => u.PassportInfo)
                                 .FirstOrDefaultAsync(u => (Guid)u.Id == command.UserId, cancellationToken) ??
                   throw new NotFoundException(typeof(User), command.UserId);

        user.UpdateName(new UserFirstName(command.FirstName), new UserLastName(command.LastName));

        var passportInfo = user.PassportInfo;
        passportInfo.PassportType = command.PassportType.ToPassportType();
        passportInfo.PassportInfoPassportNumber = new PassportInfoPassportNumber(command.PassportNumber);
        passportInfo.PassportInfoRegDate = new PassportInfoRegDate(command.RegDate);
        passportInfo.PassportInfoExpirationDate = new PassportInfoExpirationDate(command.ExpirationDate);

        user.UpdateRole(command.Role.ToRole());

        user.UpdateBirthDay(new UserBirthDay(command.Birthdate));

        await _context.SaveChangesAsync(cancellationToken);

        return new UserDto(user.Id, user.UserFirstName, user.UserLastName, user.PassportInfo.PassportInfoPassportNumber, user.PassportInfo.PassportType.Name, user.Role.Name);
    }
}
