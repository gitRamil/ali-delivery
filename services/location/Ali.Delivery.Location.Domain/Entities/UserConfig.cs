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
    public UserConfig(SequentialGuid id, LanguageDictionary language)
        : base(id) =>
        Language = language ?? throw new ArgumentNullException(nameof(language));

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="UserConfig" /> для Entity Framework.
    /// </summary>
    /// <remarks>
    /// Этот конструктор предназначен только для использования Entity Framework и не должен вызываться напрямую.
    /// </remarks>
    protected UserConfig()
        : base(SequentialGuid.Empty) =>
        Language = null!;

    /// <summary>
    /// Язык интерфейса пользователя.
    /// </summary>
    public virtual LanguageDictionary Language { get; private set; }

    /// <summary>
    /// Навигационное свойство User для правильной связи 1:1.
    /// </summary>
    public virtual User? User { get; }

    /// <summary>
    /// Обновляет язык в конфигурации пользователя.
    /// </summary>
    /// <param name="newLanguage">Новый язык для установки.</param>
    public void UpdateLanguage(LanguageDictionary newLanguage)
    {
        Language = newLanguage ?? throw new ArgumentNullException(nameof(newLanguage));
    }
}
