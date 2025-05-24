namespace Ali.Delivery.Location.Infrastructure.Services.MyBotClient;

public interface IMyBotClient
{
    public Task RunBot(CancellationToken cancellationToken);
}
