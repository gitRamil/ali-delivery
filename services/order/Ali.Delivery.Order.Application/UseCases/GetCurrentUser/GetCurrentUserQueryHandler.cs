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
public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserDto>
{
    private readonly IAppDbContext _context;
    private readonly ICurrentUser _currentUser;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="GetCurrentUserQueryHandler" />.
    /// </summary>
    /// <param name="query">Контекст БД.</param>
    /// <param name="currentUser">Текущий пользователь.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="query" /> или
    /// <paramref name="currentUser" /> равен <c>null</c>.
    /// </exception>
    public GetCurrentUserQueryHandler(IAppDbContext query, ICurrentUser currentUser)
    {
        _context = query ?? throw new ArgumentNullException(nameof(query));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="request" /> равен <c>null</c>.
    /// </exception>
    public async Task<UserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = await _context.Users.FirstOrDefaultAsync(o => (Guid)o.Id == _currentUser.Id, cancellationToken) ?? throw new NotFoundException(typeof(User), _currentUser.Id);

        return new UserDto(user.Id,
                           user.Login,
                           user.FirstName,
                           user.LastName,
                           user.PassportInfo?.PassportNumber,
                           user.PassportInfo?.PassportType.Name,
                           user.Role.Name,
                           user.PassportInfo?.RegDate,
                           user.PassportInfo?.IssuedBy);
    }
}
