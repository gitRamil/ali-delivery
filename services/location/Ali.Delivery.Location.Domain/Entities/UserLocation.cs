using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;

namespace Ali.Delivery.Location.Domain.Entities;

/// <summary>
/// Представляет сущность локации пользователя.
/// </summary>
public class UserLocation : Entity<SequentialGuid>
{
    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="UserLocation" />.
    /// </summary>
    /// <param name="id">Идентификатор локации пользователя.</param>
    /// <param name="telegramLogin">Телеграм логин.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="telegramLogin" /> равен <c>null</c>.
    /// </exception>
    public UserLocation(SequentialGuid id, string telegramLogin)
        : base(id) =>
        TelegramLogin = telegramLogin ?? throw new ArgumentNullException(nameof(telegramLogin));

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="UserLocation" /> для использования ORM.
    /// </summary>
    /// <remarks>Конструктор без параметров необходим для Entity Framework.</remarks>
    protected UserLocation()
        : base(SequentialGuid.Empty) =>
        TelegramLogin = null!;

    /// <summary>
    /// Возвращает координаты долготы.
    /// </summary>
    public string? E { get; set; }

    /// <summary>
    /// Возвращает координаты широты.
    /// </summary>
    public string? S { get; set; }

    /// <summary>
    /// Возвращает пользователя.
    /// </summary>
    public string TelegramLogin { get; }

    /// <summary>
    /// Обновляет координаты пользователя.
    /// </summary>
    /// <param name="newE">Новые координаты долготы.</param>
    /// <param name="newS">Новые координаты широты.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="newS" /> или
    /// <paramref name="newE" /> равен <c>null</c>.
    /// </exception>
    public void UpdateCoordinates(string? newE, string? newS)
    {
        E = newE ?? throw new ArgumentNullException(nameof(newE));
        S = newS ?? throw new ArgumentNullException(nameof(newS));
    }
}
