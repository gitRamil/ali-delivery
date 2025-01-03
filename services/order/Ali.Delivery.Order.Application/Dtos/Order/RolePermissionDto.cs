namespace Ali.Delivery.Order.Application.Dtos.Order;

/// <summary>
/// Представляет связь между ролью и разрешением.
/// </summary>
/// <param name="Id">Идентификатор связи.</param>
/// <param name="RoleId">Идентификатор роли.</param>
/// <param name="PermissionId">Идентификатор разрешения.</param>
/// <param name="Token">JWT токен, связанный с разрешением роли.</param>
public sealed record RolePermissionDto(Guid Id, Guid RoleId, Guid PermissionId, string Token);
