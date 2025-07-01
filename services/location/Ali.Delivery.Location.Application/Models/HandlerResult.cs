namespace Ali.Delivery.Location.Application.Models;

/// <summary>
/// Представляет результат работы обработчика шага (<see cref="Abstractions.IStepHandler"/>),
/// содержащий опцию или триггер для перехода в следующее состояние.
/// </summary>
/// <param name="NextStepOption">
/// Строковая опция, которую машина состояний (<see cref="Abstractions.IStateMachine"/>) будет использовать 
/// для поиска следующего шага в своей конфигурации.
/// </param>
public record HandlerResult(string NextStepOption);
