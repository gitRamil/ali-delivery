using System.Runtime.Serialization;

namespace Ali.Delivery.Order.Application.Dtos.Order;

public enum PassportTypeCodeEnum
{
    /// <summary>
    /// Согласована.
    /// </summary>
    [EnumMember(Value = "internal")]
    Internal,

    /// <summary>
    /// Отклонена.
    /// </summary>
    [EnumMember(Value = "international")]
    International,
    
    /// <summary>
    /// Отклонена.
    /// </summary>
    [EnumMember(Value = "diplomatic")]
    Diplomatic,
}