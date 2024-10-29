using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.Size;

namespace Ali.Delivery.Order.Domain.Entities.Dictionaries;

/// <summary>
/// Типы размеров заказа.
/// </summary>
public class Size : Entity<SequentialGuid>
{
    /// <summary>
    /// Возвращает размер: Large.
    /// </summary>
    public static readonly Size Large = new(new Guid("3a156e1f-6057-39cd-7580-20395231a00f"), new SizeCode("large"), new SizeName("Большой"));

    /// <summary>
    /// Возвращает размер: Medium.
    /// </summary>
    public static readonly Size Medium = new(new Guid("3a156e1f-6056-875d-e42d-3e8e7ec6e082"), new SizeCode("medium"), new SizeName("Средний"));

    /// <summary>
    /// Возвращает размер: Small.
    /// </summary>
    public static readonly Size Small = new(new Guid("3a156e1f-6055-abfb-36b7-7e630cc807b9"), new SizeCode("small"), new SizeName("Маленький"));

    private static readonly Dictionary<SizeCode, Size> Sizes = new()
    {
        [Small.Code] = Small,
        [Medium.Code] = Medium,
        [Large.Code] = Large
    };

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="Size" />.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="code">Код размера.</param>
    /// <param name="name">Наименование размера.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="code" /> или
    /// <paramref name="name" /> равен <c>null</c>.
    /// </exception>
    /// <remarks>Конструктор для EF.</remarks>
    public Size(SequentialGuid id, SizeCode code, SizeName name)
        : base(id)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    /// <summary>
    /// Возвращает код размера.
    /// </summary>
    public SizeCode Code { get; }

    /// <summary>
    /// Возвращает наименование размера.
    /// </summary>
    public SizeName Name { get; }

    /// <summary>
    /// Возвращает все значения перечисления размеров.
    /// </summary>
    public static IReadOnlyCollection<Size> GetAllValues() => Sizes.Values;
}
