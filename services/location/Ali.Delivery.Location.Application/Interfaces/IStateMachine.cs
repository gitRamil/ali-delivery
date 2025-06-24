using Ali.Delivery.Location.Infrastructure.Models;

namespace Ali.Delivery.Location.Application.Interfaces;

public interface IStateMachine
{
    Task ProcessUpdateAsync(MessageInfo messageInfo, Func<Task> sendInvalidCommandMessage);
}
