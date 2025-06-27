using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Models;
using Ali.Delivery.Location.Application.Configurations;
using Microsoft.Extensions.Options;

namespace Ali.Delivery.Location.Application.StateMachine;

public class StateMachine : IStateMachine // TODO: Реализовать нормальную обработку "StopStep" и его нотификатора
{
    private readonly StateMachineConfiguration _config;
    private readonly IStepHandlerMapping _stepHandlerMapping;
    private readonly IUserStateService _userStateService;

    public StateMachine(IOptions<StateMachineConfiguration> config, IStepHandlerMapping stepHandlerMapping, IUserStateService userStateService)
    {
        _config = config.Value ?? throw new ArgumentNullException(nameof(config));
        _stepHandlerMapping = stepHandlerMapping ?? throw new ArgumentNullException(nameof(stepHandlerMapping));
        _userStateService = userStateService ?? throw new ArgumentNullException(nameof(userStateService));
    }

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
