using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.User
{
    /// <summary>
    /// Представляет email пользователя.
    /// </summary>
    [DebuggerDisplay("{_email}")]
    public class UserEmail : ValueObject
    {
        /// <summary>
        /// Регулярное выражение для проверки формата email.
        /// </summary>
        private const string EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        private readonly string _email;

        /// <summary>
        /// Инициализирует новый экземпляр типа <see cref="UserEmail" />.
        /// </summary>
        /// <param name="email">Email пользователя.</param>
        /// <exception cref="ArgumentException">
        /// Возникает, если <paramref name="email" /> является <c>null</c> или не соответствует формату.
        /// </exception>
        public UserEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email не может быть пустым или null.", nameof(email));
            }

            email = email.Trim();

            if (!Regex.IsMatch(email, EmailPattern))
            {
                throw new ArgumentException("Email имеет неверный формат.", nameof(email));
            }

            _email = email;
        }

        /// <inheritdoc />
        public override string ToString() => _email;

        /// <summary>
        /// Возвращает набор компонентов, участвующих в сравнении.
        /// </summary>
        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return _email;
        }

        /// <summary>
        /// Выполняет неявное преобразование из <see cref="UserEmail" /> в <see cref="string" />.
        /// </summary>
        /// <param name="obj">Email пользователя.</param>
        [return: NotNullIfNotNull(nameof(obj))]
        public static implicit operator string?(UserEmail? obj) => obj?._email;
    }
}
