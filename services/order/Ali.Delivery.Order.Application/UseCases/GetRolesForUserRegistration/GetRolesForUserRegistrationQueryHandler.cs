using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.GetRolesForUserRegistration;

/// <summary>
/// Представляет обработчик команды получения всех ролей для регистрации пользователя.
/// </summary>
public class GetRolesForUserRegistrationQueryHandler : IRequestHandler<GetRolesForUserRegistrationQuery, List<RoleDto>>
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="GetRolesForUserRegistrationQueryHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    public GetRolesForUserRegistrationQueryHandler(IAppDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc />
    public async Task<List<RoleDto>> Handle(GetRolesForUserRegistrationQuery query, CancellationToken cancellationToken)
    {
        return await _context.Roles.Select(r => new RoleDto(r.Code, r.Name))
                             .ToListAsync(cancellationToken);
    }
}
