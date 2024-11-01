using System.Runtime.Serialization;

namespace Ali.Delivery.Order.Application.Dtos.Order;

/// <summary>
/// Представляет набор значений, описывающих статусы заказа.
/// </summary>
public enum SizeCodeEnum
{
    /// <summary>
    /// Согласована.
    /// </summary>
    [EnumMember(Value = "large")]
    Large,

    /// <summary>
    /// Отклонена.
    /// </summary>
    [EnumMember(Value = "small")]
    Small,
    
    /// <summary>
    /// Отклонена.
    /// </summary>
    [EnumMember(Value = "medium")]
    Medium
}
