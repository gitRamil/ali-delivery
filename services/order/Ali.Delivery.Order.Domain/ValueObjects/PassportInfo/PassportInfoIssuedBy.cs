using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.PassportInfo;

/// <summary>
/// Представляет строку "Кем выдан".
/// </summary>
[DebuggerDisplay("{_issuedBy}")]
public class PassportInfoIssuedBy : ValueObject
{
    /// <summary>
    /// Максимальная длина адреса.
    /// </summary>
    public const int MaxLength = 250;

    private readonly string _issuedBy;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="PassportInfoIssuedBy" />.
    /// </summary>
    /// <param name="issuedBy">Кем выдан.</param>
    /// <exception cref="ArgumentException">Возникает, если строка пуста или превышает максимальную длину.</exception>
    public PassportInfoIssuedBy(string issuedBy)
    {
        if (string.IsNullOrWhiteSpace(issuedBy))
        {
            throw new ArgumentException("Строка не может быть пустой.", nameof(issuedBy));
        }

        if (issuedBy.Length > MaxLength)
        {
            throw new ArgumentException($"Строка не может быть длиннее {MaxLength} символов.", nameof(issuedBy));
        }

        _issuedBy = issuedBy;
    }

    /// <inheritdoc />
    public override string ToString() => _issuedBy;

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _issuedBy;
    }
    
    /// <summary>
    /// Выполняет явное преобразование из <see cref="string" /> в <see cref="PassportInfoIssuedBy" />.
    /// </summary>
    /// <param name="obj">Номер паспорта.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static explicit operator PassportInfoIssuedBy?(string? obj) => obj == null ? null : new PassportInfoIssuedBy(obj);

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="PassportInfoIssuedBy" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Номер паспорта.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(PassportInfoIssuedBy? obj) => obj?._issuedBy;
}
