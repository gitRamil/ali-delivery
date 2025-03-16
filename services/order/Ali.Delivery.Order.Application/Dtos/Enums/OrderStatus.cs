using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Generators;

namespace Ali.Delivery.Order.Application.Dtos.Enums;

/// <summary>
/// Представляет набор значений, описывающих статусы заказа.
/// </summary>
[MapDictionaryEntity(typeof(OrderStatus))]
public enum OrderStatusCode
{
    /// <summary>
    /// Создан.
    /// </summary>
    Created,

    /// <summary>
    /// В процессе.
    /// </summary>
    InProgress,

    /// <summary>
    /// Завершен.
    /// </summary>
    Finished
}
