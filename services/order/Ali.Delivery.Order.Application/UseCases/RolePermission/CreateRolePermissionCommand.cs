using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.RolePermission;

/// <summary>
/// Команда для создания нового разрешения для роли.
/// </summary>
public record CreateRolePermissionCommand(RoleCode Role, Dtos.Order.PermissionCode Permission) : IRequest<Guid>;
