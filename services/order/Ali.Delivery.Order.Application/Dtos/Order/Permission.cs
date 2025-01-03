using System.Runtime.Serialization;

namespace Ali.Delivery.Order.Application.Dtos.Order;

/// <summary>
/// 
/// </summary>
public enum PermissionCode
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    [EnumMember(Value = "createUser")]
    CreateUser,

    /// <summary>
    /// Курьер.
    /// </summary>
    [EnumMember(Value = "updateOrder")]
    UpdateOrder,

    /// <summary>
    /// Неавторизованный пользователь.
    /// </summary>
    [EnumMember(Value = "getOrder")]
    GetOrder,
    
    /// <summary>
    /// Неавторизованный пользователь.
    /// </summary>
    [EnumMember(Value = "deleteOrder")]
    DeleteOrder,
}
