using Ali.Delivery.Location.Application.Models;

namespace Ali.Delivery.Location.Application.Abstractions;

public interface IStateMachine
{
    Task ProcessUpdateAsync(MessageInfo messageInfo, Func<Task> sendInvalidCommandMessage);
}
