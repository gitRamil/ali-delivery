using System.Runtime.Serialization;

namespace Ali.Delivery.Order.Application.Dtos.Order;

/// <summary>
/// Представляет набор значений, описывающих тип паспорта.
/// </summary>
public enum PassportType
{
    /// <summary>
    /// Внутренний.
    /// </summary>
    [EnumMember(Value = "internal")]
    Internal,

    /// <summary>
    /// Зарубежный.
    /// </summary>
    [EnumMember(Value = "international")]
    International,

    /// <summary>
    /// Дипломатический.
    /// </summary>
    [EnumMember(Value = "diplomatic")]
    Diplomatic
}
