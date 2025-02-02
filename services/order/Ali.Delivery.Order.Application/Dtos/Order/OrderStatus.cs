using System.Runtime.Serialization;

namespace Ali.Delivery.Order.Application.Dtos.Order;

/// <summary>
/// Представляет набор значений, описывающих статусы заказа.
/// </summary>
public enum OrderStatus
{
    /// <summary>
    /// Создан.
    /// </summary>
    [EnumMember(Value = "created")]
    Created,

    /// <summary>
    /// В процессе.
    /// </summary>
    [EnumMember(Value = "inProgress")]
    InProgress,

    /// <summary>
    /// Завершен.
    /// </summary>
    [EnumMember(Value = "finished")]
    Finished
}
