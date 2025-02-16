using System.Diagnostics;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.PassportInfo;

/// <summary>
/// Представляет номер паспорта.
/// </summary>
[DebuggerDisplay("{_passportNumber}")]
public sealed class PassportInfoPassportNumber : ValueObject
{
    /// <summary>
    /// Максимальная длина номера паспорта.
    /// </summary>
    public const int MaxLength = 20;

    private readonly string _passportNumber;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="PassportInfoPassportNumber" />.
    /// </summary>
    /// <param name="passportNumber">Номер паспорта.</param>
    /// <exception cref="ArgumentException">Возникает, если номер паспорта пуст или превышает максимальную длину.</exception>
    public PassportInfoPassportNumber(string? passportNumber)
    {
        if (string.IsNullOrWhiteSpace(passportNumber))
        {
            throw new ArgumentException("Номер паспорта не может быть пустым.", nameof(passportNumber));
        }

        if (passportNumber.Length > MaxLength)
        {
            throw new ArgumentException($"Номер паспорта не может быть длиннее {MaxLength} символов.", nameof(passportNumber));
        }

        _passportNumber = passportNumber;
    }

    /// <inheritdoc />
    public override string ToString() => _passportNumber;

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _passportNumber;
    }

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="PassportInfoPassportNumber" /> в <see cref="string" />.
    /// </summary>
    public static implicit operator string?(PassportInfoPassportNumber? obj) => obj?._passportNumber ?? null;
}
