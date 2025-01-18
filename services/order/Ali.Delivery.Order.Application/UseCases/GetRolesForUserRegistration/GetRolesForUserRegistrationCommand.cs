using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.GetRolesForUserRegistration;

/// <summary>
/// Представляет команду получения всех ролей для регистрации пользователя.
/// </summary>
public record GetRolesForUserRegistrationCommand : IRequest<List<RoleDto>>;
