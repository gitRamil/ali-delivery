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
    public CreateUserCommandHandler(IAppDbContext context) =>
        _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <summary>
    /// Выполняет команду создания пользователя.
    /// </summary>
    /// <param name="request">Команда.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="request" /> равен <c>null</c>.
    /// </exception>
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        // Создание PassportInfo на основе данных из команды
        var passportInfo = new PassportInfo(
            SequentialGuid.Create(),
            request.PassportType.ToPassportType(), 
            new PassportNumber(request.PassportNumber),
            new RegDate(request.RegDate),
            new ExpirationDate(request.ExpirationDate)
        );

        // Создание Role на основе кода
        var role = request.Role.ToRole(); 

        // Создание пользователя
        var user = new User(
            SequentialGuid.Create(),
            new FirstName(request.FirstName),
            new LastName(request.LastName),
            passportInfo,
            role,
            new Birthday(request.Birthday)
        );

        _context.Users.Add(user);

        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
