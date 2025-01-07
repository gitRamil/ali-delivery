using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.User;

/// <summary>
/// Представляет логин пользователя.
/// </summary>
[DebuggerDisplay("{_login}")]
public class UserLogin : ValueObject
{
    /// <summary>
    /// Максимальная длина логина
    /// </summary>
    public const int MaxLength = 100;
    private readonly string _login;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="UserLogin" />.
    /// </summary>
    /// <param name="login">Логин пользователя.</param>
    /// <exception cref="ArgumentException">
    /// Возникает, если <paramref name="login" /> является <c>null</c>,
    /// <c>whitespace</c> или его длина превышает <see cref="MaxLength" />.
    /// </exception>
    public UserLogin(string login)
    {
        if (string.IsNullOrWhiteSpace(login))
        {
            throw new ArgumentException("Логин не может быть пустой или null.", nameof(login));
        }

        login = login.Trim();

        if (login.Length > MaxLength)
        {
            throw new ArgumentException($"Логин не может быть длиннее {MaxLength} символов.", nameof(login));
        }

        _login = login;
    }

    /// <inheritdoc />
    public override string ToString() => _login;

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _login;
    }

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="UserLogin" /> в <see cref="string" />.
    /// </summary>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(UserLogin? obj) => obj?._login;
}
