using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Application.Exceptions;
using Ali.Delivery.Order.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.GetCurrentUser;

/// <summary>
/// Представляет обработчик запроса на получение текущего пользователя.
/// </summary>
public class GetCurrentUserCommandHandler : IRequestHandler<GetCurrentUserCommand, UserDto>
{
    private readonly IAppDbContext _context;
    private readonly ICurrentUser _currentUser;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="GetCurrentUserCommandHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <param name="currentUser">Текущий пользователь.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public GetCurrentUserCommandHandler(IAppDbContext context, ICurrentUser currentUser)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="request" /> равен <c>null</c>.
    /// </exception>
    public async Task<UserDto> Handle(GetCurrentUserCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var user = await _context.Users.FirstOrDefaultAsync(o => (Guid)o.Id == _currentUser.Id, cancellationToken) ?? throw new NotFoundException(typeof(User), _currentUser.Id);

        return new UserDto(
            user.Id,
            user.Login,
            user.UserFirstName?.ToString() ?? "",  
            user.UserLastName?.ToString() ?? "",   
            user.PassportInfo?.PassportInfoPassportNumber ?? "",  
            user.PassportInfo?.PassportType.Name ?? "",  
            user.Role.Name
        );
        
    }
}
