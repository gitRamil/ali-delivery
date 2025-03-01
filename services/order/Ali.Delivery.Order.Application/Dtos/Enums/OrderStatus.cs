namespace Ali.Delivery.Order.Application.Dtos.Enums;

/// <summary>
/// Представляет набор значений, описывающих статусы заказа.
/// </summary>
public enum OrderStatus
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
