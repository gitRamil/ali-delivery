using Ali.Delivery.Location.Application.Models;
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
    Task<AuthenticationResult> AuthenticateAsync(string login, string password);

    /// <summary>
    /// Асинхронно обрабатывает команду входа от пользователя, полученную из чата.
    /// </summary>
    /// <param name="chatId">Уникальный идентификатор чата с пользователем.</param>
    /// <param name="text">Текст сообщения, предположительно содержащий логин и пароль.</param>
    /// <param name="cancellationToken">Маркет отмены.</param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию.
    /// Результат задачи содержит объект <see cref="CommandResult" />, который может включать информацию о следующем шаге или
    /// результате операции для пользователя.
    /// </returns>
    Task<CommandResult> LoginAsync(long chatId, string text, CancellationToken cancellationToken);
}
