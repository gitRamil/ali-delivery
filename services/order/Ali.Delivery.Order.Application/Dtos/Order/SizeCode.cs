using System.Runtime.Serialization;

namespace Ali.Delivery.Order.Application.Dtos.Order;

/// <summary>
/// Представляет набор значений, описывающих размер посылки.
/// </summary>
public enum SizeCode
{
    /// <summary>
    /// Большая.
    /// </summary>
    [EnumMember(Value = "large")]
    Large,

    /// <summary>
    /// Маленькая.
    /// </summary>
    [EnumMember(Value = "small")]
    Small,

    /// <summary>
    /// Средняя.
    /// </summary>
    [EnumMember(Value = "medium")]
    Medium
}
