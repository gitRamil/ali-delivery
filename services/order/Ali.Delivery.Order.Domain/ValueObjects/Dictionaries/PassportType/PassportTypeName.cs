using System.Diagnostics.CodeAnalysis;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.PassportType;

/// <summary>
/// Представляет наименование типа паспорта.
/// </summary>
public sealed class PassportTypeName : ValueObject
{
    /// <summary>
    /// Представляет максимальную длину наименования типа паспорта.
    /// </summary>
    public const int MaxLength = 200;

    private readonly string _name;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="PassportTypeName" />.
    /// </summary>
    /// <param name="name">Наименование типа паспорта.</param>
    /// <exception cref="ArgumentException">
    /// Возникает, если <paramref name="name" /> является <c>null</c> или <c>whitespace</c> или его длина превышает <see cref="MaxLength" />.
    /// </exception>
    public PassportTypeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Наименование типа паспорта не может быть null или пустой строкой.", nameof(name));
        }

        name = name.Trim();

        if (name.Length > MaxLength)
        {
            throw new ArgumentException($"Наименование типа паспорта не может быть больше {MaxLength}.", nameof(name));
        }

        _name = name;
    }

    /// <summary>Возвращает строковое представление объекта.</summary>
    public override string ToString() => _name;

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _name;
    }

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="PassportTypeName" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Значение наименования типа паспорта.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(PassportTypeName? obj) => obj?._name;
}