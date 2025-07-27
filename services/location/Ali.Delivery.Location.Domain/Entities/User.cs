using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Location.Domain.Entities.Dictionaries;

namespace Ali.Delivery.Location.Domain.Entities;

/// <summary>
/// Представляет сущность пользователя.
/// </summary>
public class User : Entity<SequentialGuid>
{
    private UserConfig? _userConfig;
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

    /// <summary>
    /// Получает конфигурацию пользователя.
    /// </summary>
    /// <value>
    /// Коллекция объектов <see cref="UserConfig" />.
    /// </value>
    public virtual UserConfig? UserConfig
    {
        get => _userConfig;
        private set => _userConfig = value;
    }

    /// <summary>
    /// Получает коллекцию местоположений пользователя только для чтения.
    /// </summary>
    /// <value>
    /// Коллекция объектов <see cref="UserLocation" />.
    /// </value>
    public virtual IReadOnlyCollection<UserLocation> UserLocations => _userLocations;

    /// <summary>
    /// Добавляет новую запись в таблицу UserLocation.
    /// </summary>
    /// <param name="longitude">Долгота.</param>
    /// <param name="latitude">Широта.</param>
    /// <exception cref="ArgumentException">Если код языка неизвестен.</exception>
    public void AddUserLocation(double longitude, double latitude)
    {
        var userLocation = new UserLocation(SequentialGuid.Create(), this, longitude, latitude);
        _userLocations.Add(userLocation);
    }

    /// <summary>
    /// Добавляет или обновляет конфигурацию пользователя с выбранным языком.
    /// Если конфигурация уже существует, обновляет только язык. Если язык не изменился, ничего не делает.
    /// </summary>
    /// <param name="languageCode">Код языка (например, "ru" или "en"). Если не указан, будет использован "ru".</param>
    /// <exception cref="ArgumentException">Если код языка неизвестен.</exception>
    public void UpsertUserLanguage(string? languageCode)
    {
        var languageDict = LanguageDictionary.FromCode(languageCode ?? "ru");

        if (_userConfig != null)
        {
            if (_userConfig.Language.Id == languageDict.Id)
            {
                return;
            }

            _userConfig.UpdateLanguage(languageDict);
        }
        else
        {
            _userConfig = new UserConfig(Id, languageDict);
        }
    }
}
