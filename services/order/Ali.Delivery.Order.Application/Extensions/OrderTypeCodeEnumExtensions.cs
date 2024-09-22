using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;

namespace Ali.Delivery.Order.Application.Extensions;

public static class OrderTypeCodeEnumExtensions
{
    /// <summary>
    /// Преобразует значение <see cref="OrderStatusCode" /> в значение <see cref="OrderStatus" />.
    /// </summary>
    /// <param name="code">Значение.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Возникает, если значение не поддерживается.
    /// </exception>
    public static OrderStatus ToRatingCoefficient(this OrderStatusCode code) =>
        code switch
        {
            OrderStatusCode.Created => OrderStatus.Created,
            OrderStatusCode.Finished => OrderStatus.Finished,
            _ => throw new ArgumentOutOfRangeException(nameof(code), code, "Не поддерживаемое значение типа OrderStatusCode.")
        };

    /// <summary>
    /// Преобразует значение <see cref="OrderStatus" /> в значение <see cref="OrderStatusCode" />.
    /// </summary>
    /// <param name="value">Значение.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Возникает, если значение не поддерживается.
    /// </exception>
    public static OrderStatusCode ToRatingCoefficientCode(this OrderStatus value) =>
        value switch
        {
            not null when value == OrderStatus.Created => OrderStatusCode.Created,
            not null when value == OrderStatus.Finished => OrderStatusCode.Finished,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Не поддерживаемое значение типа OrderStatus.")
        };
}
