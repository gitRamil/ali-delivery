using Ali.Delivery.Location.Application.Models;

namespace Ali.Delivery.Location.Application.Abstractions;

/// <summary>
/// Представляет контракт для конечного автомата, управляющего состоянием пользователей и переходами между ними.
/// </summary>
public interface IStateMachine
{
    /// <summary>
    /// Асинхронно обрабатывает входящее сообщение от пользователя, определяя текущее состояние,
    /// вызывая соответствующий обработчик и выполняя переход в следующее состояние.
    /// </summary>
    /// <param name="messageInfo">Объект, содержащий всю информацию о входящем сообщении и пользователе.</param>
    /// <param name="sendInvalidCommandMessage">
    /// Функция обратного вызова (callback), которая будет выполнена,
    /// если машина состояний не сможет определить следующий шаг (например, при неверной команде).
    /// </param>
    Task ProcessUpdateAsync(MessageInfo messageInfo, Func<Task> sendInvalidCommandMessage);
}
