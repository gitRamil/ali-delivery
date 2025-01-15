using System.Runtime.Serialization;

namespace Ali.Delivery.Order.Application.Dtos.Order;

/// <summary>
/// Коды доступов.
/// </summary>
public enum PermissionCode
{
    /// <summary>
    /// Создать пользователя.
    /// </summary>
    [EnumMember(Value = "userManagement")]
    UserManagement,

    /// <summary>
    /// Обновить заказ.
    /// </summary>
    [EnumMember(Value = "orderManagement")]
    OrderManagement,

    /// <summary>
    /// Получить заказ.
    /// </summary>
    [EnumMember(Value = "fullAccess")]
    FullAccess,

    /// <summary>
    /// Удалить заказ.
    /// </summary>
    [EnumMember(Value = "tracking")]
    Tracking
}
