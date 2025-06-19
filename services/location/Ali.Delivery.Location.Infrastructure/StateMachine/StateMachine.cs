using Ali.Delivery.Location.Infrastructure.Interfaces;
using Ali.Delivery.Location.Infrastructure.Models;
using Ali.Delivery.Location.Infrastructure.Models.Configuration;
using Microsoft.Extensions.Options;

namespace Ali.Delivery.Location.Infrastructure.StateMachine;

public class StateMachine : IStateMachine
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
        var text = messageInfo.Text;

        if (string.Equals(text, "/stop", StringComparison.OrdinalIgnoreCase))
        {
            await _userStateService.SetUserStepAsync(userId, "StopStep");
            return;
        }

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
