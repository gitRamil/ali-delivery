using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Location.Domain.Entities.Dictionaries;

namespace Ali.Delivery.Location.Domain.Entities;

/// <summary>
/// Представляет сущность конфигураций пользователя.
/// </summary>
public class UserConfig : Entity<SequentialGuid>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="UserConfig" />.
    /// </summary>
    /// <param name="id">Уникальный идентификатор конфигурации пользователя.</param>
    /// <param name="language">Язык интерфейса пользователя.</param>
    /// <param name="user">Пользователь, к которому относится данная конфигурация.</param>
    /// <exception cref="ArgumentNullException">
    /// Выбрасывается, когда <paramref name="language" /> или <paramref name="user" /> равны <c>null</c>.
    /// </exception>
    public UserConfig(SequentialGuid id, LanguageDictionary language, User user)
        : base(id)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
        Language = language ?? throw new ArgumentNullException(nameof(language));
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="UserConfig" /> для Entity Framework.
    /// </summary>
    /// <remarks>
    /// Этот конструктор предназначен только для использования Entity Framework и не должен вызываться напрямую.
    /// </remarks>
    protected UserConfig()
        : base(SequentialGuid.Empty)
    {
        User = null!;
        Language = null!;
    }

    /// <summary>
    /// Язык интерфейса пользователя.
    /// </summary>
    public virtual LanguageDictionary Language { get; private set; }

    /// <summary>
    /// Пользователь, к которому относится данная конфигурация.
    /// </summary>
    public virtual User User { get; private set; }

    /// <summary>
    /// Обновляет язык в конфигурации пользователя.
    /// </summary>
    /// <param name="newLanguage">Новый язык для установки.</param>
    public void UpdateLanguage(LanguageDictionary newLanguage)
    {
        Language = newLanguage ?? throw new ArgumentNullException(nameof(newLanguage));
    }
}
