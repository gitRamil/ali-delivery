using System.Runtime.Serialization;

namespace Ali.Delivery.Order.Application.Dtos.Order;

/// <summary>
/// Представляет набор значений, описывающих статусы заказа.
/// </summary>
public enum OrderStatusCode
{
    /// <summary>
    /// Согласована.
    /// </summary>
    [EnumMember(Value = "created")]
    Created,

    /// <summary>
    /// Отклонена.
    /// </summary>
    [EnumMember(Value = "finished")]
    Finished
}
