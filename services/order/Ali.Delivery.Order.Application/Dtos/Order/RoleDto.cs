namespace Ali.Delivery.Order.Application.Dtos.Order;

/// <summary>
/// Представляет набор значений, описывающих роли пользователей.
/// </summary>
/// <param name="Code">Код роли.</param>
/// <param name="Name">Наименование роли.</param>
public sealed record RoleDto(string Code, string Name);
