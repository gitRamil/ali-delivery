using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Application;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Enums;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Infrastructure.services;

/// <summary>
/// Представляет провайдер значений справочника.
/// </summary>
public sealed class DictionaryValuesProvider : IDictionaryValuesProvider
{
    private readonly IDictionaryTypeMap _typeMap;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="DictionaryValuesProvider" />.
    /// </summary>
    /// <param name="typeMap">Карта наборов справочников.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="typeMap" /> равен <c>null</c>.
    /// </exception>
    public DictionaryValuesProvider(IDictionaryTypeMap typeMap) => _typeMap = typeMap ?? throw new ArgumentNullException(nameof(typeMap));

    /// <summary>
    /// Возвращает значения справочника.
    /// </summary>
    /// <param name="code">Код справочника.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// </exception>
    public async Task<IReadOnlyList<Entity<SequentialGuid>>> GetDictionaryValuesAsync(DictionaryCode code, CancellationToken cancellationToken) =>
        await _typeMap.GetDictionaryQueryable(code)
                      .ToListAsync(cancellationToken);
}
