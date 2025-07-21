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
    /// <param name="user">Id пользователя.</param>
    /// <param name="longitude">Долгота.</param>
    /// <param name="latitude">Широта.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="user" /> равен <c>null</c>.
    /// </exception>
    public UserLocation(SequentialGuid id, User user, double longitude, double latitude)
        : base(id)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
        Longitude = longitude;
        Latitude = latitude;
    }

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="UserLocation" /> для использования ORM.
    /// </summary>
    /// <remarks>Конструктор без параметров необходим для Entity Framework.</remarks>
    protected UserLocation()
        : base(SequentialGuid.Empty)
    {
        Longitude = 0;
        Latitude = 0;
        User = null!;
    }

    /// <summary>
    /// Возвращает координаты широты.
    /// </summary>
    public double Latitude { get; private set; }

    /// <summary>
    /// Возвращает координаты долготы.
    /// </summary>
    public double Longitude { get; private set; }

    /// <summary>
    /// Возвращает пользователя.
    /// </summary>
    public virtual User User { get; private set; }
}
