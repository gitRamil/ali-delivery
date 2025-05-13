using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;

namespace Ali.Delivery.Location.Domain.Entities;

public class UserLocation : Entity<SequentialGuid>
{
    public UserLocation(SequentialGuid id, string telegramLogin)
        : base(id)
    {
        TelegramLogin = telegramLogin  ?? throw new ArgumentNullException(nameof(telegramLogin));
    }

    protected UserLocation()
        : base(SequentialGuid.Empty)
    {
        TelegramLogin = null!;
    }
    
    /// <summary>
    /// Возвращает координаты пользователя.
    /// </summary>
    public string? E { get; set; }

    /// <summary>
    /// Возвращает координаты пользователя.
    /// </summary>
    public string? S { get; set; }

    /// <summary>
    /// Возвращает пользователя.
    /// </summary>
    public string TelegramLogin { get; }
    
    public void UpdateCoordinates(string? newE, string? newS)
    {
        E = newE;
        S = newS;
    }
    
   
}
