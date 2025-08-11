using Ali.Delivery.Location.Application.Models.Authentication;

namespace Ali.Delivery.Location.Application.Abstractions;

/// <summary>
/// Представляет контракт для сервиса, отвечающего за аутентификацию пользователей.
/// </summary>
public interface IAuthenticationService // TODO: Подумать над упрощением.
{
    /// <summary>
    /// Асинхронно выполняет основную проверку учетных данных пользователя.
    /// </summary>
    /// <param name="login">Логин пользователя.</param>
    /// <param name="password">Пароль пользователя.</param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию.
    /// Результат задачи содержит объект <see cref="AuthenticationResult" /> с итогами проверки.
    /// </returns>
    Task<AuthenticationResult> GetAuthenticationResult(string login, string password);
}
