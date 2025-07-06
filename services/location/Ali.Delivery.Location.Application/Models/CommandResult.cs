namespace Ali.Delivery.Location.Application.Models;

/// <summary>
/// Представляет результат выполнения команды, содержащий ключ для определения следующего шага в логике приложения.
/// </summary>
/// <param name="NextStepKey">Ключ, используемый для определения следующего состояния или шага.</param>
public record CommandResult(string NextStepKey);
