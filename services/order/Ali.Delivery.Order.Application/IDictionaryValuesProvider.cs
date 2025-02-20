using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order.Enum;

namespace Ali.Delivery.Order.Application;

/// <summary>
/// Описывает провайдер значений справочника.
/// </summary>
public interface IDictionaryValuesProvider
{
    /// <summary>
    /// Возвращает значения справочника.
    /// </summary>
    /// <param name="code">Код справочника.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    Task<IReadOnlyList<Entity<SequentialGuid>>> GetDictionaryValuesAsync(DictionaryCode code, CancellationToken cancellationToken);
}
