using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.User;

/// <summary>
/// Представляет пароль пользователя.
/// </summary>
[DebuggerDisplay("{_password}")]
public class UserPassword: ValueObject
{
    /// <summary>
    /// Представляет максимальную длину имени пользователя.
    /// </summary>
    public const int MaxLength = 100;

    private readonly string _password;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="UserPassword" />.
    /// </summary>
    /// <param name="password">Пароль пользователя.</param>
    /// <exception cref="ArgumentException">
    /// Возникает, если <paramref name="password" /> является <c>null</c>,
    /// <c>whitespace</c> или его длина превышает <see cref="MaxLength" />.
    /// </exception>
    public UserPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Пароль не может быть пустым или null.", nameof(password));
        }

        password = password.Trim();

        if (password.Length > MaxLength)
        {
            throw new ArgumentException($"Пароль не может быть длиннее {MaxLength} символов.", nameof(password));
        }

        _password = GenerateHash(password);
    }

    private static string GenerateHash(string password) => BCrypt.Net.BCrypt.EnhancedHashPassword(password);

    private bool VerifyPassword(string password, string hashedPassword) => BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);

    /// <inheritdoc />
    public override string ToString() => _password;

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _password;
    }

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="UserPassword" /> в <see cref="string" />.
    /// </summary>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(UserPassword? obj) => obj?._password;
}
