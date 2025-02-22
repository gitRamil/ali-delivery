using System.Runtime.Serialization;

namespace Ali.Delivery.Order.Application.Dtos.Enums;

/// <summary>
/// Коды доступов.
/// </summary>
public enum PermissionCode
{
    /// <summary>
    /// Управление пользователем.
    /// </summary>
    [EnumMember(Value = "userManagement")]
    UserManagement,

    /// <summary>
    /// Управление заказами.
    /// </summary>
    [EnumMember(Value = "orderManagement")]
    OrderManagement,

    /// <summary>
    /// Полный доступ.
    /// </summary>
    [EnumMember(Value = "fullAccess")]
    FullAccess,

    /// <summary>
    /// Отслеживание.
    /// </summary>
    [EnumMember(Value = "tracking")]
    Tracking,

    /// <summary>
    /// Управление заказами для пользователя.
    /// </summary>
    [EnumMember(Value = "UserOrderManagement")]
    UserOrderManagement,

    /// <summary>
    /// Управления заказами для курьера.
    /// </summary>
    [EnumMember(Value = "courierOrderManagement")]
    CourierOrderManagement
}
