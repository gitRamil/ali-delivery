using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.NotAuthUser;

/// <summary>
/// Представляет имя незарегистрированного пользователя.
/// </summary>
[DebuggerDisplay("{_name}")]
public class NotAuthUserFirstName : ValueObject
{
    /// <summary>
    /// Представляет максимальную длину имени незарегистрированного пользователя.
    /// </summary>
    public const int MaxLength = 100;

    private readonly string _name;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="NotAuthUserFirstName" />.
    /// </summary>
    /// <param name="name">Имя незарегистрированного пользователя.</param>
    /// <exception cref="ArgumentException">
    /// Возникает, если <paramref name="name" /> является <c>null</c>,
    /// <c>whitespace</c> или его длина превышает <see cref="MaxLength" />.
    /// </exception>
    public NotAuthUserFirstName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Имя незарегистрированного пользователя не может быть null или пустой строкой.", nameof(name));
        }
        
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
    /// Выполняет явное преобразование из <see cref="string" /> в <see cref="NotAuthUserFirstName" />.
    /// </summary>
    /// <param name="obj">Номер паспорта.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static explicit operator NotAuthUserFirstName?(string? obj) => obj == null ? null : new NotAuthUserFirstName(obj);

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="NotAuthUserFirstName" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Номер паспорта.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(NotAuthUserFirstName? obj) => obj?._name;
}
