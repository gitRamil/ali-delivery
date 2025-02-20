using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order.Enum;

namespace Ali.Delivery.Order.Application.Services;

/// <summary>
/// Представляет карту соответствия кода справочника к набору сущностей справочника.
/// </summary>
public sealed class DictionaryTypeMap : IDictionaryTypeMap
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="DictionaryTypeMap" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public DictionaryTypeMap(IAppDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <summary>
    /// Возвращает набор сущностей, соответствующий коду справочника.
    /// </summary>
    /// <param name="code">Код справочника.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Возникает, если передан не поддерживаемый тип справочника.
    /// </exception>
    public IQueryable<Entity<SequentialGuid>> GetDictionaryQueryable(DictionaryCode code) =>
        code switch
        {
            DictionaryCode.OrderStatus => _context.OrderStatuses,
            DictionaryCode.Role => _context.Roles,
            DictionaryCode.PassportType => _context.PassportTypes,
            DictionaryCode.Size => _context.Sizes,
            _ => throw new ArgumentOutOfRangeException(nameof(code), code, "Не поддерживаемый тип справочника.")
        };
}
