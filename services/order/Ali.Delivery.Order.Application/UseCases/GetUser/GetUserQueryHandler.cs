using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Application.Exceptions;
using Ali.Delivery.Order.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.GetUser;

/// <summary>
/// Представляет обработчик запроса на получение пользователя.
/// </summary>
public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="GetUserQueryHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public GetUserQueryHandler(IAppDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="query" /> равен <c>null</c>.
    /// </exception>
    public async Task<UserDto> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        var user = await _context.Users.FirstOrDefaultAsync(o => (Guid)o.Id == query.UserId, cancellationToken) ?? throw new NotFoundException(typeof(User), query.UserId);

        return new UserDto(user.Id,
                           user.Login,
                           user.UserFirstName,
                           user.UserLastName,
                           user.PassportInfo?.PassportInfoPassportNumber!,
                           user.PassportInfo?.PassportType.Name,
                           user.Role.Name,
                           user.PassportInfo?.PassportInfoRegDate,
                           user.PassportInfo?.PassportInfoIssuedBy);
    }
}
