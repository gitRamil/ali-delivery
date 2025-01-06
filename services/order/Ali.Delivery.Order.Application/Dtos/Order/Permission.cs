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
    [EnumMember(Value = "createUser")]
    CreateUser,

    /// <summary>
    /// Обновить заказ.
    /// </summary>
    [EnumMember(Value = "updateOrder")]
    UpdateOrder,

    /// <summary>
    /// Получить заказ.
    /// </summary>
    [EnumMember(Value = "getOrder")]
    GetOrder,

    /// <summary>
    /// Удалить заказ.
    /// </summary>
    [EnumMember(Value = "deleteOrder")]
    DeleteOrder
}
