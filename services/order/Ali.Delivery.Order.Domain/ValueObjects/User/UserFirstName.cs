using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.User;

/// <summary>
/// Представляет имя пользователя.
/// </summary>
[DebuggerDisplay("{_name}")]
public class UserFirstName : ValueObject
{
    /// <summary>
    /// Представляет максимальную длину имени пользователя.
    /// </summary>
    public const int MaxLength = 100;

    private readonly string _name;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="UserFirstName" />.
    /// </summary>
    /// <param name="name">Имя пользователя.</param>
    /// <exception cref="ArgumentException">
    /// Возникает, если <paramref name="name" /> является <c>null</c>,
    /// <c>whitespace</c> или его длина превышает <see cref="MaxLength" />.
    /// </exception>
    public UserFirstName(string name)
    {
        name = name.Trim();

        if (name.Length > MaxLength)
        {
            throw new ArgumentException($"Имя не может быть длиннее {MaxLength} символов.", nameof(name));
        }

        _name = name;
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
    /// Выполняет неявное преобразование из <see cref="UserFirstName" /> в <see cref="string" />.
    /// </summary>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(UserFirstName? obj) => obj?._name;
}
