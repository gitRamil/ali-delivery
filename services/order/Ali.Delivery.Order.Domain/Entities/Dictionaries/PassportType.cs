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
    /// Возвращает тип паспорта: Дипломатический.
    /// </summary>
    public static readonly PassportType Diplomatic = new(new Guid("3a15d9e1-c99f-95c5-162b-34f69121c4a1"),
                                                         new PassportTypeCode("diplomatic"),
                                                         new PassportTypeName("Дипломатический"));

    /// <summary>
    /// Возвращает тип паспорта: Внутренний.
    /// </summary>
    public static readonly PassportType Internal = new(new Guid("3a15d9e1-c9a0-80ab-eac9-9369b2ace783"), new PassportTypeCode("internal"), new PassportTypeName("Внутренний"));

    /// <summary>
    /// Возвращает тип паспорта: Заграничный.
    /// </summary>
    public static readonly PassportType International = new(new Guid("3a15d9e1-c9a1-8b19-64f4-8cb3007b8a5d"),
                                                            new PassportTypeCode("international"),
                                                            new PassportTypeName("Заграничный"));

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
