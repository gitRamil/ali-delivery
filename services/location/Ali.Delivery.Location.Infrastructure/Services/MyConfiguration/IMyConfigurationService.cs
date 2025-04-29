namespace Ali.Delivery.Location.Infrastructure.Services.MyConfiguration;

public interface IMyConfigurationService
{
    public string GetConnectionString();
    public string GetDatabaseConnectionString();
    public string GetTgToken();
}
