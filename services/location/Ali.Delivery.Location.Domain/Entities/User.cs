using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;

namespace Ali.Delivery.Location.Domain.Entities;

/// <summary>
/// Представляет сущность пользователя.
/// </summary>
public class User : Entity<SequentialGuid>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="User" />.
    /// </summary>
    /// <param name="id">Уникальный идентификатор пользователя.</param>
    /// <param name="login">Логин пользователя в системе.</param>
    /// <param name="chatId">Идентификатор чата пользователя в мессенджере.</param>
    /// <exception cref="ArgumentNullException">
    /// Выбрасывается, когда <paramref name="login" /> или <paramref name="chatId" /> равны <c>null</c>.
    /// </exception>
    public User(SequentialGuid id, string login, string chatId)
        : base(id)
    {
        Login = login ?? throw new ArgumentNullException(nameof(login));
        ChatId = chatId ?? throw new ArgumentNullException(nameof(chatId));
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="User" /> для Entity Framework.
    /// </summary>
    /// <remarks>
    /// Этот конструктор предназначен только для использования Entity Framework и не должен вызываться напрямую.
    /// </remarks>
    protected User()
        : base(SequentialGuid.Empty)
    {
        Login = null!;
        ChatId = null!;
    }

    /// <summary>
    /// Идентификатор чата пользователя в мессенджере.
    /// </summary>
    public string ChatId { get; private set; }

    /// <summary>
    /// Логин пользователя в системе.
    /// </summary>
    public string Login { get; private set; }
}
