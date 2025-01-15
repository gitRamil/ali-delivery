using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.GetAllUsers;

/// <summary>
/// Представляет обработчик запроса на получение списка всех пользователей.
/// </summary>
public class GetAllUsersCommandHandler : IRequestHandler<GetAllUsers, List<UserDto>>
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="GetAllUsersCommandHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public GetAllUsersCommandHandler(IAppDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc />
    public async Task<List<UserDto>> Handle(GetAllUsers request, CancellationToken cancellationToken)
    {
        var users = await _context.Users.Select(user => new UserDto(user.Id,
                                                                    user.UserFirstName,
                                                                    user.UserLastName,
                                                                    user.PassportInfo.PassportInfoPassportNumber,
                                                                    user.PassportInfo.PassportType.Name,
                                                                    user.Role.Name))
                                  .ToListAsync(cancellationToken);

        return users;
    }
}
