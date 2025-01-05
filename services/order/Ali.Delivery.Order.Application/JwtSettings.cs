namespace Ali.Delivery.Order.Application;

/// <summary>
/// Представляет настройки, необходимые для создания JWT-токенов.
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// Получает или задает количество часов до истечения срока действия токена.
    /// </summary>
    public int ExpiresHours { get; set; }

    /// <summary>
    /// Получает или задает секретный ключ, используемый для подписи токена.
    /// </summary>
    public string SecretKey { get; set; }
}
