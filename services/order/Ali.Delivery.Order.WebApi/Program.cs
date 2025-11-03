using System.Text.Json;
using System.Text.Json.Serialization;
using Ali.Delivery.Order.Application;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Services;
using Ali.Delivery.Order.Infrastructure.services;
using Ali.Delivery.Order.WebApi.Infrastructure.IoC;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.CookiePolicy;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Configuration.AddEnvironmentVariables("AliDeliveryOrderService_");
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

    builder.Services.AddSignalRServices(builder.Configuration);
    builder.Services.AddSignalRCorsPolicy("http://localhost:3000", "https://localhost:3000");

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDateTimeService();
    builder.Services.AddDefaultProblemDetails();
    builder.Services.AddScoped<JwtProvider>();
    builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
    builder.Services.AddApiAuthentication();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddTransient<IDictionaryValuesProvider, DictionaryValuesProvider>();
    builder.Services.AddTransient<IDictionaryTypeMap, DictionaryTypeMap>();
    builder.Services.AddTransient<ICurrentUser, CurrentUserService>();
    builder.Services.AddRabbitMqService(builder.Configuration);

    var app = builder.Build();
    app.UseHttpsRedirection();
    app.AddAutomaticMigrations();

    var consumer = app.Services.GetRequiredService<IMessageConsumer>();
    await consumer.StartAsync();

    app.Lifetime.ApplicationStopping.Register(async () =>
    {
        await consumer.StopAsync();
    });

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseSerilogRequestLogging();
    app.UseProblemDetails();
    app.UseRouting();

    app.UseCors("SignalRCorsPolicy");

    app.UseCookiePolicy(new CookiePolicyOptions
    {
        MinimumSameSitePolicy = SameSiteMode.Strict,
        HttpOnly = HttpOnlyPolicy.Always,
        Secure = CookieSecurePolicy.None // для http
    });

    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.MapSignalRHubs();

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
