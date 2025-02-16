using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.GetAllRoles;

/// <summary>
/// Представляет обработчик команды получения всех ролей пользователя.
/// </summary>
public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, List<RoleDto>>
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="GetAllRolesQueryHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    public GetAllRolesQueryHandler(IAppDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc />
    public async Task<List<RoleDto>> Handle(GetAllRolesQuery query, CancellationToken cancellationToken)
    {
        return await _context.Roles.Select(r => new RoleDto(r.Code, r.Name))
                             .ToListAsync(cancellationToken);
    }
}
