using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Extensions;
using Ali.Delivery.Order.Domain.ValueObjects.PassportInfo;
using Ali.Delivery.Order.Domain.ValueObjects.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.CompletePassport;

/// <summary>
/// Представляет обработчик команды заполнения паспорта.
/// </summary>
public class CompletePassportCommandHandler : IRequestHandler<CompletePassportCommand, Guid>
{
    private readonly IAppDbContext _context;
    private readonly ICurrentUser _currentUser;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="CompletePassportCommandHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <param name="currentUser">Текущий пользователь.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> или
    /// <paramref name="currentUser" /> равен <c>null</c>.
    /// </exception>
    public CompletePassportCommandHandler(IAppDbContext context, ICurrentUser currentUser)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    /// <inheritdoc />
    public async Task<Guid> Handle(CompletePassportCommand command, CancellationToken cancellationToken)
    {
        var user = await _context.Users.Include(u => u.PassportInfo)
                                 .FirstOrDefaultAsync(u => (Guid)u.Id == _currentUser.Id, cancellationToken) ??
                   throw new InvalidOperationException("Пользователь не найден.");

        user.CreatePassportInfo(SequentialGuid.Create(),
                                command.PassportType.ToPassportType(),
                                new PassportInfoPassportNumber(command.PassportNumber),
                                new PassportInfoRegDate(command.RegDate),
                                new PassportInfoIssuedBy(command.IssuedBy));
        user.UpdateName(new UserFirstName(command.FirstName), new UserLastName(command.LastName));

        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
