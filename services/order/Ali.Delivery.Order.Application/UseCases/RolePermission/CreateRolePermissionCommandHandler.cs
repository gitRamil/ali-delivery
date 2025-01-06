using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Extensions;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.RolePermission;

/// <summary>
/// Обработчик команды для создания разрешения для роли.
/// </summary>
public class CreateRolePermissionCommandHandler : IRequestHandler<CreateRolePermissionCommand, Guid>
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Конструктор обработчика команды <see cref="CreateRolePermissionCommandHandler" />.
    /// </summary>
    /// <param name="context">Контекст приложения.</param>
    public CreateRolePermissionCommandHandler(IAppDbContext context) => _context = context;

    /// <summary>
    /// Обрабатывает команду для создания связи между ролью и разрешением.
    /// </summary>
    /// <param name="request">Команда создания разрешения для роли.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор созданной связи.</returns>
    public async Task<Guid> Handle(CreateRolePermissionCommand request, CancellationToken cancellationToken)
    {
        var permission = request.Permission.ToPermission();
        var role = request.Role.ToRole();
        var rolePermission = new Domain.Entities.RolePermission(SequentialGuid.Create(), permission.Id, role.Id);

        _context.RolePermissions.Add(rolePermission);
        await _context.SaveChangesAsync(cancellationToken);

        return rolePermission.Id;
    }
}
