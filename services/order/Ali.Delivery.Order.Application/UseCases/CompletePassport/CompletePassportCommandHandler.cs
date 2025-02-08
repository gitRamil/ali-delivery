using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Extensions;
using Ali.Delivery.Order.Domain.Entities;
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
    public CompletePassportCommandHandler(IAppDbContext context, ICurrentUser currentUser)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    /// <inheritdoc />
    public async Task<Guid> Handle(CompletePassportCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.Id;

        var user = await _context.Users.Include(u => u.PassportInfo)
                                 .FirstOrDefaultAsync(u => (Guid)u.Id == userId, cancellationToken) ??
                   throw new InvalidOperationException("Пользователь не найден.");

        if (user.PassportInfo != null)
        {
            throw new InvalidOperationException("Паспортные данные уже заполнены, для изменения паспортных данных обратитесь в поддержку");
        }
        
        user.UpdateName(new UserFirstName(request.FirstName),new UserLastName(request.LastName));
        user.PassportInfo = new PassportInfo(SequentialGuid.Create(),
                                             request.PassportType.ToPassportType(),
                                             new PassportInfoPassportNumber(request.PassportNumber),
                                             new PassportInfoRegDate(request.RegDate),
                                             new PassportInfoIssuedBy(request.IssuedBy));
        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
