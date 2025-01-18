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
    /// Возникает, если <paramref name="request" /> равен <c>null</c>.
    /// </exception>
    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = await _context.Users.Include(u => u.Role)
                                 .Include(u => u.PassportInfo)
                                 .FirstOrDefaultAsync(u => (Guid)u.Id == request.UserId, cancellationToken) ??
                   throw new NotFoundException(typeof(User), request.UserId);

        user.UpdateName(new UserFirstName(request.FirstName), new UserLastName(request.LastName));

        var passportInfo = user.PassportInfo;

        if (passportInfo != null)
        {
            passportInfo.PassportType = request.PassportType.ToPassportType();
            passportInfo.PassportInfoPassportNumber = new PassportInfoPassportNumber(request.PassportNumber);
            passportInfo.PassportInfoRegDate = new PassportInfoRegDate(request.RegDate);
            passportInfo.PassportInfoExpirationDate = new PassportInfoExpirationDate(request.ExpirationDate);
        }

        user.UpdateRole(request.Role.ToRole());

        user.UpdateBirthDay(new UserBirthDay(request.Birthdate));

        await _context.SaveChangesAsync(cancellationToken);

        return new UserDto(user.Id, user.UserFirstName, user.UserLastName, user.PassportInfo!.PassportInfoPassportNumber, user.PassportInfo.PassportType.Name, user.Role.Name);
    }
}
