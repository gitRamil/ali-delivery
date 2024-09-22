using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.OrderStatus;

/// <summary>
/// Представляет наименование справочника статусов периода целеполагания.
/// </summary>
[DebuggerDisplay("{_name}")]
public sealed class OrderStatusName : ValueObject
{
    /// <summary>
    /// Представляет максимальную длину наименования справочника статусов периода целеполагания.
    /// </summary>
    public const int MaxLength = 250;

    private readonly string _name;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="OrderStatusName" />.
    /// </summary>
    /// <param name="name">Наименование справочника статусов периода целеполагания.</param>
    /// <exception cref="ArgumentException">
    /// Возникает, если <paramref name="name" />
    /// является <c>null</c> или <c>whitespace</c> или его длина превышает <see cref="MaxLength" />.
    /// </exception>
    public OrderStatusName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Наименование справочника статусов периода целеполагания не может быть null или пустой строкой.", nameof(name));
        }

        if (name.Length > MaxLength)
        {
            throw new ArgumentException($"Наименование справочника статусов периода целеполагания не может быть больше {MaxLength}.", nameof(name));
        }

        _name = name;
    }

    /// <inheritdoc />
    public override string ToString() => _name;

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _name;
    }

    /// <summary>
    /// Выполняет явное преобразование из <see cref="string" /> в <see cref="OrderStatusCode" />.
    /// </summary>
    /// <param name="obj">Описание цели.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static explicit operator OrderStatusName?(string? obj) => obj == null ? null : new OrderStatusName(obj);

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="OrderStatusCode" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Значение наименования справочника должностей.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(OrderStatusName? obj) => obj?._name;
}
