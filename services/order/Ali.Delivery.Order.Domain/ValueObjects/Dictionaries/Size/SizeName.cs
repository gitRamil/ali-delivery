using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.Size;

/// <summary>
/// Представляет наименование справочника размеров.
/// </summary>
[DebuggerDisplay("{_name}")]
public sealed class SizeName : ValueObject
{
    /// <summary>
    /// Представляет максимальную длину наименования справочника размеров.
    /// </summary>
    public const int MaxLength = 100;

    private readonly string _name;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="SizeName" />.
    /// </summary>
    /// <param name="name">Наименование справочника размеров.</param>
    /// <exception cref="ArgumentException">
    /// Возникает, если <paramref name="name" /> является <c>null</c> или <c>whitespace</c>, или его длина превышает <see cref="MaxLength" />.
    /// </exception>
    public SizeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Наименование справочника размеров не может быть null или пустой строкой.", nameof(name));
        }

        if (name.Length > MaxLength)
        {
            throw new ArgumentException($"Наименование справочника размеров не может быть больше {MaxLength}.", nameof(name));
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
    /// Выполняет явное преобразование из <see cref="string" /> в <see cref="SizeName" />.
    /// </summary>
    /// <param name="obj">Наименование размера.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static explicit operator SizeName?(string? obj) => obj == null ? null : new SizeName(obj);

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="SizeName" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Значение наименования размера.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(SizeName? obj) => obj?._name;
}
