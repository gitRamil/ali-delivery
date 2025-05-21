using System.Text.Json;
using System.Text.Json.Serialization;
using Ali.Delivery.Location.Domain.Services;
using Ali.Delivery.Location.Infrastructure;
using Ali.Delivery.Location.Infrastructure.ExternalServices;
using Ali.Delivery.Location.Infrastructure.Services.MyBotClient;
using Ali.Delivery.Location.Infrastructure.Services.MyConfiguration;
using Ali.Delivery.Location.Infrastructure.Services.WriteToDataBased;
using Ali.Delivery.Location.WebApi.Infrastructure.IoC;
using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using Refit;
using Serilog;


try
{
    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration;
    var orderServiceDbUrl = "http://localhost:5091/";
    var locationServiceDbUrl = "http://localhost:5287/";

    builder.Services.AddEndpointsApiExplorer();
    builder.Configuration.AddEnvironmentVariables("AliDeliveryLocationService_");
    builder.AddDefaultSerilog();
    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        });
    builder.Services.AddDefaultApiVersioning();
    builder.Services.AddDefaultSwagger();
    builder.Services.AddDefaultMediatr();
    builder.Services.AddDefaultEfCore();
    builder.Services.AddDefaultCorsPolicy();
    builder.Services.AddDateTimeService();
    builder.Services.AddDefaultProblemDetails();
    builder.Services.AddSwaggerGen();
    builder.Services.AddSingleton<IMyConfigurationService>(_ =>
        new MyConfigurationService(builder.Configuration));

    builder.Services.AddSingleton<IWriteToDatabase, WriteToDatabase>();
    builder.Services.AddSingleton<IMyBotClient, MyBotClient>();
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(configuration.GetConnectionString(nameof(AppDbContext)))
            .UseSnakeCaseNamingConvention()
            .EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine, LogLevel.Information));
    builder.Services.AddHostedService<BotBackgroundService>();
    builder.Services.AddRefitClient<IFileServiceForOrder>()
        .ConfigureHttpClient(c => c.BaseAddress = new Uri(orderServiceDbUrl));
    builder.Services.AddRefitClient<IFileServiceForLocation>()
           .ConfigureHttpClient(c => c.BaseAddress = new Uri(locationServiceDbUrl));
    builder.Services.AddSingleton<AuthService>();

    var app = builder.Build();
    app.AddAutomaticMigrations();
    app.UseSwagger();
    app.UseSwaggerUI();


    app.UseSerilogRequestLogging();
    // app.UseHttpsRedirection();
    app.UseProblemDetails();
    app.UseRouting();
    app.UseCors();
    app.MapControllers();


    app.Run();
    return 0;
}
catch (Exception e)
{
    Log.Fatal(e, "Хост неожиданно прекратил работу");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}

