using Ali.Delivery.Location.Application.Models;

namespace Ali.Delivery.Location.Application.Abstractions;

/// <summary>
/// Представляет контракт для обработчика логики конкретного шага (состояния) в конечном автомате.
/// </summary>
public interface IStepHandler
{
    /// <summary>
    /// Асинхронно обрабатывает сообщение пользователя для текущего шага.
    /// </summary>
    /// <param name="update">Информация о входящем сообщении от пользователя.</param>
    /// <param name="cancellationToken">Маркер отмены для прерывания асинхронной операции.</param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию.
    /// Результат задачи содержит объект <see cref="HandlerResult"/> с ключом для определения следующего шага.
    /// </returns>
    Task<HandlerResult> HandleAsync(MessageInfo update, CancellationToken cancellationToken = default);
}
