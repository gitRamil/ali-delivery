namespace Ali.Delivery.Order.Application.Dtos.Order;

/// <summary>
/// Представляет набор значений, описывающих роли пользователей.
/// </summary>
/// <param name="Code">Код.</param>
/// <param name="Name">Наименование.</param>
public sealed record RoleDto(string Code, string Name);
