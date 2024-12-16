using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.RolePermission;

/// <summary>
/// Представляет токен для разрешений роли.
/// </summary>
[DebuggerDisplay("{_token}")]
public class RolePermissionToken : ValueObject
{
    public const int TokenLength = 64; // Длина токена
    private readonly string _token;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="RolePermissionToken" />.
    /// </summary>
    /// <param name="token">Значение токена.</param>
    /// <exception cref="ArgumentException">
    /// Выбрасывается, если <paramref name="token" /> является <c>null</c>, <c>whitespace</c> или его длина не равна
    /// <see cref="TokenLength" />.
    /// </exception>
    public RolePermissionToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            throw new ArgumentException("Токен не может быть пустым или null.", nameof(token));
        }

        if (token.Length != TokenLength)
        {
            throw new ArgumentException($"Токен должен быть длиной {TokenLength} символов.", nameof(token));
        }

        _token = token;
    }

    /// <summary>
    /// Генерирует новый случайный токен.
    /// </summary>
    /// <returns>Уникальный токен длиной <see cref="TokenLength" />.</returns>
    public static RolePermissionToken Generate()
    {
        using var rng = RandomNumberGenerator.Create();
        var bytes = new byte[TokenLength / 2]; // 1 байт = 2 символа в HEX
        rng.GetBytes(bytes);

        var token = BitConverter.ToString(bytes)
                                .Replace("-", "")
                                .ToLowerInvariant();
        return new RolePermissionToken(token);
    }

    /// <inheritdoc />
    public override string ToString() => _token;

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _token;
    }

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="RolePermissionToken" /> в <see cref="string" />.
    /// </summary>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(RolePermissionToken? obj) => obj?._token;
}
