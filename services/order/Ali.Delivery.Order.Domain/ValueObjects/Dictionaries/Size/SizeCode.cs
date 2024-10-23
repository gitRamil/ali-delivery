using System.Diagnostics.CodeAnalysis;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.Size;

/// <summary>
/// Представляет код справочника размеров.
/// </summary>
public sealed class SizeCode : ValueObject
{
    /// <summary>
    /// Представляет максимальную длину кода размера.
    /// </summary>
    public const int MaxLength = 100;

    private readonly string _code;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="SizeCode"/>.
    /// </summary>
    /// <param name="code">Код справочника размеров.</param>
    /// <exception cref="ArgumentException">
    /// Возникает, если <paramref name="code" /> является <c>null</c> или <c>whitespace</c> или его длина превышает <see cref="MaxLength" />.
    /// </exception>
    public SizeCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException("Код справочника размеров не может быть null или пустой строкой.",
                nameof(code));
        }

        code = code.Trim();

        if (code.Length > MaxLength)
        {
            throw new ArgumentException($"Код справочника размеров не может быть больше {MaxLength}.", nameof(code));
        }

        _code = code;
    }

    /// <summary>Возвращает строковое представление объекта.</summary>
    public override string ToString() => _code;

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _code;
    }

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="SizeCode" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Значение кода справочника размеров.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(SizeCode? obj) => obj?._code;
}