using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Ali.Delivery.Location.Infrastructure.Services.MyConfiguration;

public class MyConfigurationService(IConfiguration configuration) : IMyConfigurationService
{
    public string GetConnectionString() => configuration.GetSection("ConnectionStrings")["AppDbContext"] ?? throw new JsonException("AppDbContext");

    public string GetDatabaseConnectionString() => configuration.GetSection("ConnectionStrings")["OrderBaseConnection"]  ?? throw new JsonException("OrderBaseConnection");

    public string GetTgToken() => configuration.GetSection("ConnectionStrings")["TelegramToken"] ?? throw new JsonException("TelegramToken");
}