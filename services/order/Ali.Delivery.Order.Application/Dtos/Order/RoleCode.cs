using System.Runtime.Serialization;

namespace Ali.Delivery.Order.Application.Dtos.Order;

/// <summary>
/// Представляет набор значений, описывающих роли пользователей.
/// </summary>
public enum RoleCode
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    [EnumMember(Value = "basicUser")]
    BasicUser,

    /// <summary>
    /// Курьер.
    /// </summary>
    [EnumMember(Value = "courier")]
    Courier,

    /// <summary>
    /// Неавторизованный пользователь.
    /// </summary>
    [EnumMember(Value = "notAuthUser")]
    NotAuthUser,
}
