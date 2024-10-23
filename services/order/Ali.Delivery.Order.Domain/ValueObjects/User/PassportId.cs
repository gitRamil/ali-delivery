using System.Diagnostics;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.User;

/// <summary>
/// Представляет паспортный номер.
/// </summary>
[DebuggerDisplay("{_passportId}")]
public class PassportId : ValueObject
{
    public const int MaxLength = 20;

    private readonly string _passportId;

    public PassportId(string passportId)
    {
        if (string.IsNullOrWhiteSpace(passportId))
        {
            throw new ArgumentException("Паспортный номер не может быть пустым или null.", nameof(passportId));
        }

        passportId = passportId.Trim();

        if (passportId.Length > MaxLength)
        {
            throw new ArgumentException($"Паспортный номер не может быть длиннее {MaxLength} символов.", nameof(passportId));
        }

        _passportId = passportId;
    }

    public override string ToString() => _passportId;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _passportId;
    }

    public static implicit operator string?(PassportId? obj) => obj?._passportId;
}