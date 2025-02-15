using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.User;

/// <summary>
/// Представляет фамилию пользователя.
/// </summary>
[DebuggerDisplay("{_name}")]
public class UserLastName : ValueObject
{
    /// <summary>
    /// Максимальная длина фамилии.
    /// </summary>
    public const int MaxLength = 100;

    private readonly string _name;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="UserLastName" />.
    /// </summary>
    /// <param name="name">Фамилия пользователя.</param>
    /// <exception cref="ArgumentException">
    /// Возникает, если <paramref name="name" /> является <c>null</c>,
    /// <c>whitespace</c> или его длина превышает <see cref="MaxLength" />.
    /// </exception>
    public UserLastName(string? name)
    {
        _name = name ?? throw new ArgumentNullException(nameof(name));
        name = name.Trim();

        if (name.Length > MaxLength)
        {
            throw new ArgumentException($"Фамилия не может быть длиннее {MaxLength} символов.", nameof(name));
        }
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
    /// Выполняет неявное преобразование из <see cref="UserLastName" /> в <see cref="string" />.
    /// </summary>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(UserLastName? obj) => obj?._name;
}
