using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.GetRolesForUserRegistration;

/// <summary>
/// Представляет обработчик команды получения всех ролей для регистрации пользователя.
/// </summary>
public class GetRolesCommandHandler : IRequestHandler<GetRolesForUserRegistrationCommand, List<RoleDto>>
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="GetRolesCommandHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    public GetRolesCommandHandler(IAppDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc />
    public async Task<List<RoleDto>> Handle(GetRolesForUserRegistrationCommand request, CancellationToken cancellationToken)
    {
        var roles = await _context.Roles.Where(role => role == Role.BasicUser && role == Role.Courier)
                                  .Select(role => new RoleDto
                                  {
                                      Code = role.Code,
                                      Description = role.Name
                                  })
                                  .ToListAsync(cancellationToken);

        return roles;
    }
}
