using System.Diagnostics;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.PassportInfo;

/// <summary>
/// Представляет номер паспорта.
/// </summary>
[DebuggerDisplay("{_passportNumber}")]
public sealed class PassportNumber : ValueObject
{
    /// <summary>
    /// Максимальная длина номера паспорта.
    /// </summary>
    public const int MaxLength = 20;

    private readonly string _passportNumber;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="PassportNumber" />.
    /// </summary>
    /// <param name="passportNumber">Номер паспорта.</param>
    /// <exception cref="ArgumentException">Возникает, если номер паспорта пуст или превышает максимальную длину.</exception>
    public PassportNumber(string passportNumber)
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

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _passportNumber;
    }

    public static implicit operator string(PassportNumber obj) => obj._passportNumber;
}