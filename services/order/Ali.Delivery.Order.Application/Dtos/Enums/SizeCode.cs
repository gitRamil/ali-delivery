using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Generators;

namespace Ali.Delivery.Order.Application.Dtos.Enums;

/// <summary>
/// Представляет набор значений, описывающих размер посылки.
/// </summary>
[MapDictionaryEntity(typeof(Size))]
public enum SizeCode
{
    /// <summary>
    /// Большая.
    /// </summary>
    Large,

    /// <summary>
    /// Маленькая.
    /// </summary>
    Small,

    /// <summary>
    /// Средняя.
    /// </summary>
    Medium
}
