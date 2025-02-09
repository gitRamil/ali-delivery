using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.User;

/// <summary>
/// Представляет объект-значение для хранения и проверки пароля пользователя.
/// </summary>
[DebuggerDisplay("{_password}")]
public class UserPassword : ValueObject
{
    /// <summary>
    /// Максимальная длина пароля.
    /// </summary>
    public const int MaxLength = 100;

    private readonly string _password;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="UserPassword" />.
    /// </summary>
    /// <param name="password">Пароль пользователя.</param>
    /// <exception cref="ArgumentException">
    /// Выбрасывается, если пароль пустой, равен null или превышает допустимую длину.
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

    /// <summary>
    /// Проверяет, соответствует ли указанный пароль хэшу текущего объекта.
    /// </summary>
    /// <param name="password">Пароль для проверки.</param>
    /// <returns>
    /// <c>true</c>, если пароль совпадает с хэшем; иначе — <c>false</c>.
    /// </returns>
    public bool IsValidPassword(string password) => BCrypt.Net.BCrypt.EnhancedVerify(password, _password);

    /// <summary>
    /// Возвращает строковое представление хэша пароля.
    /// </summary>
    /// <returns>Хэш пароля в виде строки.</returns>
    public override string ToString() => _password;

    /// <summary>
    /// Возвращает компоненты, участвующие в сравнении объектов.
    /// </summary>
    /// <returns>Компоненты для сравнения.</returns>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _password;
    }

    /// <summary>
    /// Генерирует хэш для указанного пароля.
    /// </summary>
    /// <param name="password">Пароль для хэширования.</param>
    /// <returns>Хэш пароля.</returns>
    private static string GenerateHash(string password) => BCrypt.Net.BCrypt.EnhancedHashPassword(password);

    /// <summary>
    /// Преобразует объект <see cref="UserPassword" /> в строку хэша пароля.
    /// </summary>
    /// <param name="obj">Объект <see cref="UserPassword" />.</param>
    /// <returns>Строковое представление хэша пароля или <c>null</c>, если объект равен <c>null</c>.</returns>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(UserPassword? obj) => obj?._password;
}
