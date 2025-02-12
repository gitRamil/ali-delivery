using Ali.Delivery.Order.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.GetIsUserExist;

/// <summary>
/// Представляет обработчик на получение запроса о существовании пользователя в системе.
/// </summary>
public class GetIsUserExistQueryHandler : IRequestHandler<GetIsUserExistQuery, bool>
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="GetIsUserExistQueryHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public GetIsUserExistQueryHandler(IAppDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="query" /> равен <c>null</c>.
    /// </exception>
    public async Task<bool> Handle(GetIsUserExistQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        return await _context.Users.AnyAsync(u => (Guid)u.Id == query.UserId, cancellationToken);
    }
}
