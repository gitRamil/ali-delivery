using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.PassportType;

namespace Ali.Delivery.Order.Domain.Entities.Dictionaries;

/// <summary>
/// Типы паспортов.
/// </summary>
public class PassportType : Entity<SequentialGuid>
{
    /// <summary>
    /// Возвращает тип паспорта: Внутренний.
    /// </summary>
    public static readonly PassportType Internal = new PassportType(new Guid("e4a1537e-abf2-4f1a-b6f7-73343e6bf4cd"), new PassportTypeCode("internal"), new PassportTypeName("Внутренний"));

    /// <summary>
    /// Возвращает тип паспорта: Заграничный.
    /// </summary>
    public static readonly PassportType International = new PassportType(new Guid("e4a1537f-5fb2-4c9a-98e6-b03345a6d5df"), new PassportTypeCode("international"), new PassportTypeName("Заграничный"));
    
    /// <summary>
    /// Возвращает тип паспорта: Дипломатический.
    /// </summary>
    public static readonly PassportType Diplomatic = new PassportType(new Guid("e4a15380-80ab-453b-9f7e-09e136a4ef2a"), new PassportTypeCode("diplomatic"), new PassportTypeName("Дипломатический"));

    private static readonly Dictionary<PassportTypeCode, PassportType> PassportTypes = new()
    {
        [Internal.Code] = Internal,
        [International.Code] = International,
        [Diplomatic.Code] = Diplomatic
    };

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="PassportType" />.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="code">Код типа паспорта.</param>
    /// <param name="name">Наименование типа паспорта.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="code" /> или
    /// <paramref name="name" /> равен <c>null</c>.
    /// </exception>
    /// <remarks>Конструктор для EF.</remarks>
    public PassportType(SequentialGuid id, PassportTypeCode code, PassportTypeName name)
        : base(id)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    /// <summary>
    /// Возвращает код.
    /// </summary>
    public PassportTypeCode Code { get; }

    /// <summary>
    /// Возвращает наименование.
    /// </summary>
    public PassportTypeName Name { get; }

    /// <summary>
    /// Возвращает все значения перечисления.
    /// </summary>
    public static IReadOnlyCollection<PassportType> GetAllValues() => PassportTypes.Values;
}
