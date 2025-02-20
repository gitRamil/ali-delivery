using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Application.Dtos.Order.Enum;

namespace Ali.Delivery.Order.Application.Abstractions;

/// <summary>
/// Описывает карту соответствия кода справочника к набору сущностей справочника.
/// </summary>
public interface IDictionaryTypeMap
{
    /// <summary>
    /// Возвращает набор сущностей, соответствующий коду справочника.
    /// </summary>
    /// <param name="code">Код справочника.</param>
    IQueryable<Entity<SequentialGuid>> GetDictionaryQueryable(DictionaryCode code);
}
