using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Location.Domain.ValueObjects.Dictionaries.LanguageDictionary;

/// <summary>
/// Представляет наименование справочника языков.
/// </summary>
[DebuggerDisplay("{_name}")]
public sealed class LanguageName : ValueObject
{
    /// <summary>
    /// Представляет максимальную длину наименования справочника языков.
    /// </summary>
    public const int MaxLength = 20;

    private readonly string _name;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="LanguageName" />.
    /// </summary>
    /// <param name="name">Наименование справочника языков.</param>
    /// <exception cref="ArgumentException">
    /// Возникает, если <paramref name="name" /> является <c>null</c> или <c>whitespace</c>, или его длина превышает
    /// <see cref="MaxLength" />.
    /// </exception>
    public LanguageName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Наименование справочника языков не может быть null или пустой строкой.", nameof(name));
        }

        if (name.Length > MaxLength)
        {
            throw new ArgumentException($"Наименование справочника языков не может быть больше {MaxLength}.", nameof(name));
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
    /// Выполняет явное преобразование из <see cref="string" /> в <see cref="LanguageName" />.
    /// </summary>
    /// <param name="obj">Наименование языка.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static explicit operator LanguageName?(string? obj) => obj == null ? null : new LanguageName(obj);

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="LanguageName" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Значение наименования языка.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(LanguageName? obj) => obj?._name;
}
