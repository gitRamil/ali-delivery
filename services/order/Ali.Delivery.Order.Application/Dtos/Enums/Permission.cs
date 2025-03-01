namespace Ali.Delivery.Order.Application.Dtos.Enums;

/// <summary>
/// Коды доступов.
/// </summary>
public enum PermissionCode
{
    /// <summary>
    /// Управление пользователем.
    /// </summary>
    UserManagement,

    /// <summary>
    /// Управление заказами.
    /// </summary>
    OrderManagement,

    /// <summary>
    /// Полный доступ.
    /// </summary>
    FullAccess,

    /// <summary>
    /// Отслеживание.
    /// </summary>
    Tracking,

    /// <summary>
    /// Управление заказами для пользователя.
    /// </summary>
    UserOrderManagement,

    /// <summary>
    /// Управления заказами для курьера.
    /// </summary>
    CourierOrderManagement
}
