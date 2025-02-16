using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.GetAllRoles;

/// <summary>
/// Представляет команду получения всех ролей пользователя.
/// </summary>
public record GetAllRolesQuery : IRequest<List<RoleDto>>;
