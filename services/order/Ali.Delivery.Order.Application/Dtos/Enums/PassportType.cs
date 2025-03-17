using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Generators;

namespace Ali.Delivery.Order.Application.Dtos.Enums;

/// <summary>
/// Представляет набор значений, описывающих тип паспорта.
/// </summary>
[MapDictionaryEntity(typeof(PassportType))]
public enum PassportTypeCode
{
    /// <summary>
    /// Внутренний.
    /// </summary>
    Internal,

    /// <summary>
    /// Зарубежный.
    /// </summary>
    International,

    /// <summary>
    /// Дипломатический.
    /// </summary>
    Diplomatic
}
