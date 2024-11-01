using System.Runtime.Serialization;

namespace Ali.Delivery.Order.Application.Dtos.Order;

public enum RoleCodeEnum
{
    /// <summary>
    /// Согласована.
    /// </summary>
    [EnumMember(Value = "basicUser")]
    BasicUser,

    /// <summary>
    /// Отклонена.
    /// </summary>
    [EnumMember(Value = "courier")]
    Courier,
    
    /// <summary>
    /// Отклонена.
    /// </summary>
    [EnumMember(Value = "notAuthUser")]
    NotAuthUser,
}