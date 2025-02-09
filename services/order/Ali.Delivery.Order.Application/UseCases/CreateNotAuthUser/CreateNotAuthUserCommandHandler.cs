using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Exceptions;
using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.NotAuthUser;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.CreateNotAuthUser;

/// <summary>
/// Представляет обработчик команды создания незарегистрированного пользователя.
/// </summary>
public class CreateNotAuthUserCommandHandler : IRequestHandler<CreateNotAuthUserCommand, Guid>
{
    private readonly IAppDbContext _context;
    private readonly ICurrentUser _currentUser;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="CreateNotAuthUserCommandHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <param name="currentUser">Текущий пользователь.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public CreateNotAuthUserCommandHandler(IAppDbContext context, ICurrentUser currentUser)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="request" /> равен <c>null</c>.
    /// </exception>
    public async Task<Guid> Handle(CreateNotAuthUserCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var currentUser = await _context.Users.FirstOrDefaultAsync(u => (Guid)u.Id == _currentUser.Id, cancellationToken) ?? throw new NotFoundException(typeof(User), _currentUser.Id);
        if (currentUser.Role == null || currentUser.Role != Role.BasicUser)
        {
            throw new UnauthorizedAccessException("Текущий пользователь не является базовым пользователем системы.");
        }

        if (currentUser.PassportInfo == null)
        {
            throw new InvalidOperationException("Для продолжения пожалуйста заполните паспортные данные");
        }
        // Вынести в отдельный метод.

        var notAuthUser = new NotAuthUser(SequentialGuid.Create(),
                                          Role.NotAuthUser,
                                          currentUser,
                                          new NotAuthUserFirstName(request.NotAuthUserFirstName),
                                          new NotAuthUserLastName(request.NotAuthUserLastName),
                                          new NotAuthUserPhoneNumber(request.NotAuthUserPhoneNumber));
        _context.NotAuthUsers.Add(notAuthUser);

        await _context.SaveChangesAsync(cancellationToken);

        return notAuthUser.Id; // Возвращать больше инфы использовать DTO.
    }
}
