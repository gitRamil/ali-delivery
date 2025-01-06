using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Extensions;
using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.ValueObjects.PassportInfo;
using Ali.Delivery.Order.Domain.ValueObjects.User;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.CreateUser;

/// <summary>
/// Представляет обработчик команды создания пользователя.
/// </summary>
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="CreateUserCommandHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public CreateUserCommandHandler(IAppDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="request" /> равен <c>null</c>.
    /// </exception>
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var passportInfo = new PassportInfo(SequentialGuid.Create(),
                                            request.PassportType.ToPassportType(),
                                            new PassportInfoPassportNumber(request.PassportNumber),
                                            new PassportInfoRegDate(request.RegDate),
                                            new PassportInfoExpirationDate(request.ExpirationDate));

        var user = new User(SequentialGuid.Create(),
                            new UserLogin(request.Login),
                            new UserPassword(request.Password),
                            new UserFirstName(request.FirstName),
                            new UserLastName(request.LastName),
                            passportInfo,
                            request.Role.ToRole(),
                            new UserBirthDay(request.Birthday));

        _context.Users.Add(user);

        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
