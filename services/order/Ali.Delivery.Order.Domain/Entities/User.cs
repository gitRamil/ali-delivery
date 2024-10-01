using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.PassportInfo;
using Ali.Delivery.Order.Domain.ValueObjects.User;

namespace Ali.Delivery.Order.Domain.Entities
{
    /// <summary>
    /// Представляет сущность пользователя.
    /// </summary>
    public class User : Entity<SequentialGuid>
    {
        /// <summary>
        /// Инициализирует новый экземпляр типа <see cref="User" />.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="firstName">Имя пользователя.</param>
        /// <param name="lastName">Фамилия пользователя.</param>
        /// <param name="passportInfo">Паспортные данные.</param>
        /// <param name="email">Электронная почта пользователя.</param>
        /// <param name="phoneNumber">Номер телефона.</param>
        /// <param name="role">Идентификатор роли пользователя.</param>
        /// <param name="birthDay">Дата рождения.</param>
        public User(
            SequentialGuid id, 
            FirstName firstName, 
            LastName lastName, 
            PassportInfo passportInfo, 
            UserEmail email, 
            PhoneNumber phoneNumber, 
            Role role,
            DateTime birthDay)
            : base(id)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            PassportInfo = passportInfo ?? throw new ArgumentNullException(nameof(passportInfo));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
            RoleId = role;
            BirthDay = birthDay;
        }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public FirstName FirstName { get; }

        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        public LastName LastName { get; }

        /// <summary>
        /// Паспортные данные.
        /// </summary>
        public virtual PassportInfo PassportInfo { get; }

        /// <summary>
        /// Электронная почта пользователя.
        /// </summary>
        public UserEmail Email { get; }

        /// <summary>
        /// Номер телефона.
        /// </summary>
        public PhoneNumber PhoneNumber { get; }

        /// <summary>
        /// Идентификатор роли пользователя.
        /// </summary>
        public virtual Role RoleId { get; }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public DateTime BirthDay { get; }
    }
}
