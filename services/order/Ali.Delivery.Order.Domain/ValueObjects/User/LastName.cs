using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Ali.Delivery.Domain.Core;


namespace Ali.Delivery.Order.Domain.ValueObjects.User;


/// <summary>
/// Представляет фамилию пользователя.
/// </summary>
[DebuggerDisplay("{_name}")]
public class LastName : ValueObject
{
    /// <summary>
    /// Представляет максимальную длину фамилии пользователя.
    /// </summary>
    public const int MaxLength = 250;

    private readonly string _name;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="LastName" />.
    /// </summary>
    /// <param name="name">Фамилия пользователя.</param>
    /// <exception cref="ArgumentException">
    /// Возникает, если <paramref name="name" />
    /// является <c>null</c> или <c>whitespace</c> или его длина превышает <see cref="MaxLength" />.
    /// </exception>
    public LastName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Фамилия пользователя не может быть null или пустой строкой.", nameof(name));
        }

        name = name.Trim();

        if (name.Length > MaxLength)
        {
            throw new ArgumentException($"Фамилия пользователя не может быть больше {MaxLength}.", nameof(name));
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
    /// Выполняет неявное преобразование из <see cref="LastName" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Фамилия пользователя.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(LastName? obj) => obj?._name;
}