using System.Diagnostics;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.User;

/// <summary>
/// Представляет фамилию пользователя.
/// </summary>
[DebuggerDisplay("{_name}")]
public class LastName : ValueObject
{
    public const int MaxLength = 100;
    private readonly string _name;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="LastName" />.
    /// </summary>
    /// <param name="name">Фамилия пользователя.</param>
    /// <exception cref="ArgumentException">
    /// Возникает, если <paramref name="name" /> является <c>null</c>,
    /// <c>whitespace</c> или его длина превышает <see cref="MaxLength" />.
    /// </exception>
    public LastName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Фамилия не может быть пустой или null.", nameof(name));
        }

        name = name.Trim();

        if (name.Length > MaxLength)
        {
            throw new ArgumentException($"Фамилия не может быть длиннее {MaxLength} символов.", nameof(name));
        }

        _name = name;
    }

    /// <inheritdoc />
    public override string ToString() => _name;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _name;
    }

    public static implicit operator string?(LastName? obj) => obj?._name;
}
