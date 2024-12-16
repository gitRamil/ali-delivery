using Ali.Delivery.Domain.Core.Primitives;
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
    /// Возникает, если <paramref name="request" /> равен <c>null</c>.
    /// </exception>
    public async Task<UserDto> Handle(GetUserCommand request, CancellationToken cancellationToken)
    {
        return await _context.Users.Where(u => u.Id == (SequentialGuid)request.UserId)
                             .Select(user => new UserDto(user.Id,
                                                         user.UserFirstName,
                                                         user.UserLastName,
                                                         user.PassportInfo.PassportInfoPassportNumber,
                                                         user.PassportInfo.PassportType.Name,
                                                         user.Role.Name))
                             .FirstOrDefaultAsync(cancellationToken) ??
               throw new NotFoundException(typeof(User), request.UserId);
    }
}
