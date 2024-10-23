using System.Diagnostics.CodeAnalysis;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.PassportType;

/// <summary>
/// Представляет код справочника типов паспортов.
/// </summary>
public sealed class PassportTypeCode : ValueObject
{
    /// <summary>
    /// Представляет максимальную длину кода типов паспортов.
    /// </summary>
    public const int MaxLength = 100;

    private readonly string _code;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="PassportTypeCode" />.
    /// </summary>
    /// <param name="code">Код справочника типов паспортов.</param>
    /// <exception cref="ArgumentException">
    /// Возникает, если <paramref name="code" />
    /// является <c>null</c> или <c>whitespace</c> или его длина превышает <see cref="MaxLength" />.
    /// </exception>
    public PassportTypeCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException("Код справочника типов паспортов не может быть null или пустой строкой.", nameof(code));
        }

        code = code.Trim();

        if (code.Length > MaxLength)
        {
            throw new ArgumentException($"Код справочника типов паспортов не может быть больше {MaxLength}.", nameof(code));
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
    /// Выполняет неявное преобразование из <see cref="PassportTypeCode" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Значение кода справочника типов паспортов.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(PassportTypeCode? obj) => obj?._code;
}
