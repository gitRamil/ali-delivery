using Ali.Delivery.Location.Application.Models;
using Refit;

namespace Ali.Delivery.Location.Application.Abstractions;

/// <summary>
/// Представляет контракт для Refit-клиента, который взаимодействует с внешним сервисом заказов (Order Service).
/// </summary>
public interface IFileServiceForOrder
{
    /// <summary>
    /// Асинхронно получает информацию о текущем пользователе по его токену авторизации.
    /// </summary>
    /// <param name="authorization">
    /// Заголовок авторизации. Ожидается значение в формате "Bearer {token}".
    /// </param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию.
    /// Результат задачи содержит объект <see cref="UserInfo" />, если пользователь найден; в противном случае — <c>null</c>.
    /// </returns>
    [Get("/api/v1/user/get-current-user")]
    Task<UserInfo?> GetCurrentUserAsync([Header("Authorization")] string authorization);

    /// <summary>
    /// Асинхронно выполняет вход в систему, отправляя учетные данные пользователя.
    /// </summary>
    /// <param name="loginRequest">Объект с учетными данными (логин и пароль), который будет отправлен в теле запроса.</param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию.
    /// Результат задачи содержит токен доступа (например, JWT), если аутентификация прошла успешно; в противном случае —
    /// <c>null</c>.
    /// </returns>
    [Post("/api/v1/user/login")]
    Task<string?> LoginAsync([Body] LoginRequest loginRequest);
}
