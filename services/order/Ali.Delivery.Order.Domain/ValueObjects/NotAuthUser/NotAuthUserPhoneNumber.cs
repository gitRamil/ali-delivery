using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.NotAuthUser;

/// <summary>
/// Представляет номер телефона незарегистрированного пользователя.
/// </summary>
[DebuggerDisplay("{_phoneNumber}")]
public class NotAuthUserPhoneNumber : ValueObject
{
    /// <summary>
    /// Максимальная длина номера телефона.
    /// </summary>
    public const int MaxLength = 15;

    private readonly string _phoneNumber;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="NotAuthUserPhoneNumber" />.
    /// </summary>
    /// <param name="phoneNumber">Номер телефона незарегистрированного пользователя.</param>
    /// <exception cref="ArgumentException">
    /// Возникает, если <paramref name="phoneNumber" /> является <c>null</c>,
    /// <c>whitespace</c>, не соответствует формату или его длина превышает <see cref="MaxLength" />.
    /// </exception>
    public NotAuthUserPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            throw new ArgumentException("Телефон незарегистрированного пользователя не может быть null или пустой строкой.", nameof(phoneNumber));
        }

        phoneNumber = phoneNumber.Trim();

        if (phoneNumber.Length > MaxLength || !IsValidPhoneNumber(phoneNumber))
        {
            throw new ArgumentException($"Некорректный номер телефона. Длина не должна превышать {MaxLength} символов и должен соответствовать формату +7XXXXXXXXXX.",
                                        nameof(phoneNumber));
        }

        _phoneNumber = phoneNumber;
    }

    /// <inheritdoc />
    public override string ToString() => _phoneNumber;

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _phoneNumber;
    }

    /// <summary>
    /// Проверяет, является ли строка корректным номером телефона.
    /// </summary>
    private static bool IsValidPhoneNumber(string phoneNumber) => Regex.IsMatch(phoneNumber, @"^\+7\d{10}$");
    
    /// <summary>
    /// Выполняет явное преобразование из <see cref="string" /> в <see cref="NotAuthUserPhoneNumber" />.
    /// </summary>
    /// <param name="obj">Номер паспорта.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static explicit operator NotAuthUserPhoneNumber?(string? obj) => obj == null ? null : new NotAuthUserPhoneNumber(obj);

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="NotAuthUserPhoneNumber" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Номер паспорта.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(NotAuthUserPhoneNumber? obj) => obj?._phoneNumber;
}
