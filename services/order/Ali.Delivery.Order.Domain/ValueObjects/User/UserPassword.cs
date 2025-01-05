using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.User;

[DebuggerDisplay("{_password}")]
public class UserPassword: ValueObject
{
    public const int MaxLength = 100;

    private readonly string _password;
    
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

    public bool IsValidPassword(string password) => BCrypt.Net.BCrypt.EnhancedVerify(password, _password);

    public override string ToString() => _password;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _password;
    }
    
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(UserPassword? obj) => obj?._password;
}
