using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.User
{
    /// <summary>
    /// Представляет номер телефона пользователя.
    /// </summary>
    [DebuggerDisplay("{_phoneNumber}")]
    public class PhoneNumber : ValueObject
    {
        /// <summary>
        /// Регулярное выражение для проверки формата номера телефона.
        /// Предполагается международный формат (например, +1234567890).
        /// </summary>
        private const string PhoneNumberPattern = @"^\+?[1-9]\d{1,14}$";

        private readonly string _phoneNumber;

        /// <summary>
        /// Инициализирует новый экземпляр типа <see cref="PhoneNumber" />.
        /// </summary>
        /// <param name="phoneNumber">Номер телефона пользователя.</param>
        /// <exception cref="ArgumentException">
        /// Возникает, если <paramref name="phoneNumber" /> 
        /// не соответствует формату или является <c>null</c>.
        /// </exception>
        public PhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ArgumentException("Номер телефона не может быть пустым или null.", nameof(phoneNumber));
            }

            phoneNumber = phoneNumber.Trim();

            if (!Regex.IsMatch(phoneNumber, PhoneNumberPattern))
            {
                throw new ArgumentException("Номер телефона имеет неверный формат.", nameof(phoneNumber));
            }

            _phoneNumber = phoneNumber;
        }

        /// <inheritdoc />
        public override string ToString() => _phoneNumber;

        /// <summary>
        /// Возвращает набор компонентов, участвующих в сравнении.
        /// </summary>
        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return _phoneNumber;
        }

        /// <summary>
        /// Выполняет неявное преобразование из <see cref="PhoneNumber" /> в <see cref="string" />.
        /// </summary>
        /// <param name="obj">Номер телефона пользователя.</param>
        [return: NotNullIfNotNull(nameof(obj))]
        public static implicit operator string?(PhoneNumber? obj) => obj?._phoneNumber;
    }
}
