using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Location.Domain.Entities.Dictionaries;

namespace Ali.Delivery.Location.Domain.Entities;

/// <summary>
/// Представляет сущность пользователя.
/// </summary>
public class User : Entity<SequentialGuid>
{
    private readonly List<UserLocation> _userLocations = [];

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="User" />.
    /// </summary>
    /// <param name="id">Уникальный идентификатор пользователя.</param>
    /// <param name="login">Логин пользователя в системе.</param>
    /// <param name="chatId">Идентификатор чата пользователя в мессенджере.</param>
    /// <exception cref="ArgumentNullException">
    /// Выбрасывается, когда <paramref name="login" /> или <paramref name="chatId" /> равны <c>null</c>.
    /// </exception>
    public User(SequentialGuid id, string login, long chatId)
        : base(id)
    {
        Login = login ?? throw new ArgumentNullException(nameof(login));
        ChatId = chatId;
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="User" /> для Entity Framework.
    /// </summary>
    /// <remarks>
    /// Этот конструктор предназначен только для использования Entity Framework и не должен вызываться напрямую.
    /// </remarks>
    protected User()
        : base(SequentialGuid.Empty) =>
        Login = null!;

    /// <summary>
    /// Возвращает идентификатор чата пользователя в мессенджере.
    /// </summary>
    public long ChatId { get; private set; }

    /// <summary>
    /// Возвращает логин пользователя в системе.
    /// </summary>
    public string Login { get; private set; }

    /// <summary>
    /// Возвращает конфигурацию пользователя.
    /// </summary>
    public virtual UserConfig? UserConfig { get; private set; }

    /// <summary>
    /// Возвращает местоположения пользователя.
    /// </summary>
    public virtual IReadOnlyCollection<UserLocation> UserLocations => _userLocations;

    /// <summary>
    /// Добавляет местоположение.
    /// </summary>
    /// <param name="longitude">Долгота.</param>
    /// <param name="latitude">Широта.</param>
    public void AddUserLocation(double longitude, double latitude)
    {
        var userLocation = new UserLocation(SequentialGuid.Create(), this, longitude, latitude);
        _userLocations.Add(userLocation);
    }

    /// <summary>
    /// Добавляет или обновляет конфигурацию пользователя с выбранным языком.
    /// </summary>
    /// <param name="languageDictionary">Код языка (например, "ru" или "en"). Если не указан, будет использован "ru".</param>
    public void UpsertUserLanguage(LanguageDictionary languageDictionary)
    {
        if (UserConfig == null)
        {
            UserConfig = new UserConfig(Id, languageDictionary);
            return;
        }

        UserConfig.UpdateLanguage(languageDictionary);
    }
}
