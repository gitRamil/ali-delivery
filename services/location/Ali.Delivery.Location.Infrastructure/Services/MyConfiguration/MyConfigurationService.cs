using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Ali.Delivery.Location.Infrastructure.Services.MyConfiguration;

public class MyConfigurationService(IConfiguration configuration) : IMyConfigurationService
{
    public string GetTgToken() => configuration.GetSection("TelegramSettings")["TelegramToken"] ?? throw new JsonException("TelegramToken");
}
