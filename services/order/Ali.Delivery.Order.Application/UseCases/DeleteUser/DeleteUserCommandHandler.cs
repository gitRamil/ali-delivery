using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Application.Exceptions;
using Ali.Delivery.Order.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.DeleteUser;

/// <summary>
/// Представляет обработчик команды для удаления пользователя.
/// </summary>
public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, UserDto>
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="DeleteUserCommandHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public DeleteUserCommandHandler(IAppDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="query" /> равен <c>null</c>.
    /// </exception>
    public async Task<UserDto> Handle(DeleteUserCommand query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        var user = await _context.Users.FirstOrDefaultAsync(u => (Guid)u.Id == query.UserId, cancellationToken) ?? throw new NotFoundException(typeof(User), query.UserId);

        var userDto = new UserDto(user.Id,
                                  user.UserFirstName,
                                  user.UserLastName,
                                  user.PassportInfo.PassportInfoPassportNumber,
                                  user.PassportInfo.PassportType.Name,
                                  user.Role.Name);

        _context.Users.Remove(user);

        await _context.SaveChangesAsync(cancellationToken);

        return userDto;
    }
}
