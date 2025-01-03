using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.RolePermission;

/// <summary>
/// Команда для создания нового разрешения для роли.
/// </summary>
public record CreateRolePermissionCommand(
    RoleCode Role,
    PermissionCode Permission
) : IRequest<Guid>;
