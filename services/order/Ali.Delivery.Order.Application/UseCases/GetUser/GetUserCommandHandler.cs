using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.GetUser;

/// <summary>
/// Представляет обработчик запроса на получение пользователя.
/// </summary>
public class GetUserCommandHandler : IRequestHandler<GetUserCommand, UserDto>
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="GetUserCommandHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public GetUserCommandHandler(IAppDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <exception cref="NotFoundException"></exception>
    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="query" /> равен <c>null</c>.
    /// </exception>
    public async Task<UserDto> Handle(GetUserCommand query, CancellationToken cancellationToken)
    {

        var user = await _context.Users.Select(user=> new  UserDto(user.Id, user.UserFirstName, user.UserLastName, user.PassportInfo.PassportInfoPassportNumber, user.PassportInfo.PassportType.Name, user.Role.Name))
                                 .FirstOrDefaultAsync(cancellationToken);
        if (user == null)
        {
            throw new NotFoundException(typeof(Domain.Entities.Order), query.UserId); 
        }                                          
        return user;
    }
}
