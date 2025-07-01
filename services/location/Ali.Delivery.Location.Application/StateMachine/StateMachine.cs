using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Configurations;
using Ali.Delivery.Location.Application.Models;
using Microsoft.Extensions.Options;

namespace Ali.Delivery.Location.Application.StateMachine;

/// <summary>
/// Представляет конкретную реализацию конечного автомата, управляющего диалогом с пользователем.
/// </summary>
public class StateMachine : IStateMachine // TODO: Реализовать нормальную обработку "StopStep" и его нотификатора.
{
    private readonly StateMachineConfiguration _config;
    private readonly IStepHandlerMapping _stepHandlerMapping;
    private readonly IUserStateService _userStateService;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="StateMachine" />.
    /// </summary>
    /// <param name="config">
    /// Опции конфигурации <see cref="StateMachineConfiguration" />, содержащие карту состояний и
    /// переходов.
    /// </param>
    /// <param name="stepHandlerMapping">Сервис для сопоставления идентификатора шага с его конкретным обработчиком.</param>
    /// <param name="userStateService">Сервис для управления состоянием пользователя (текущий шаг, логин и т.д.).</param>
    public StateMachine(IOptions<StateMachineConfiguration> config, IStepHandlerMapping stepHandlerMapping, IUserStateService userStateService)
    {
        _config = config.Value ?? throw new ArgumentNullException(nameof(config));
        _stepHandlerMapping = stepHandlerMapping ?? throw new ArgumentNullException(nameof(stepHandlerMapping));
        _userStateService = userStateService ?? throw new ArgumentNullException(nameof(userStateService));
    }

    /// <inheritdoc />
    public async Task ProcessUpdateAsync(MessageInfo messageInfo, Func<Task> sendInvalidCommandMessage)
    {
        var userId = messageInfo.FromId;
        var currentUserStepId = await _userStateService.GetUserStepIdAsync(userId);
        var currentStepConfig = GetStepConfigById(currentUserStepId);
        var handler = _stepHandlerMapping.GetHandler(currentUserStepId);
        var result = await handler.HandleAsync(messageInfo);

        if (string.IsNullOrWhiteSpace(result.NextStepOption))
        {
            return;
        }

        if (!currentStepConfig.SelectNextStep.TryGetValue(result.NextStepOption, out var nextStepId))
        {
            await sendInvalidCommandMessage();
            return;
        }

        await _userStateService.SetUserStepAsync(userId, nextStepId);
    }

    private StepConfiguration GetStepConfigById(string stepId) =>
        _config.Steps.FirstOrDefault(s => s.Id == stepId) ?? throw new InvalidOperationException($"Конфигурация для шага '{stepId}' не найдена.");
}
