using System.Diagnostics.CodeAnalysis;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Location.Domain.ValueObjects.Dictionaries.LanguageDictionary;

/// <summary>
/// Представляет код справочника языков.
/// </summary>
public sealed class LanguageCode : ValueObject
{
    /// <summary>
    /// Представляет максимальную длину кода языка.
    /// </summary>
    public const int MaxLength = 3;

    private readonly string _code;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="LanguageCode" />.
    /// </summary>
    /// <param name="code">Код справочника языков.</param>
    /// <exception cref="ArgumentException">
    /// Возникает, если <paramref name="code" /> является <c>null</c> или <c>whitespace</c> или его длина превышает
    /// <see cref="MaxLength" />.
    /// </exception>
    public LanguageCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException("Код справочника языков не может быть null или пустой строкой.", nameof(code));
        }

        code = code.Trim();

        if (code.Length > MaxLength)
        {
            throw new ArgumentException($"Код справочника языков не может быть больше {MaxLength}.", nameof(code));
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
    /// Выполняет неявное преобразование из <see cref="LanguageCode" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Значение кода справочника языков.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(LanguageCode? obj) => obj?._code;
}
