using Ali.Delivery.Location.Domain.Services;
using Ali.Delivery.Location.Infrastructure;
using Ali.Delivery.Location.Infrastructure.ExternalServices;
using Ali.Delivery.Location.Infrastructure.Services.MyBotClient;
using Ali.Delivery.Location.Infrastructure.Services.MyConfiguration;
using Ali.Delivery.Location.Infrastructure.Services.WriteToDataBased;
using Microsoft.EntityFrameworkCore;
using Refit;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var userDbUrl = "http://localhost:5091/";

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMyConfigurationService>(_ =>
    new MyConfigurationService(builder.Configuration));

builder.Services.AddSingleton<IWriteToDatabase, WriteToDatabase>();
builder.Services.AddSingleton<IMyBotClient, MyBotClient>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString(nameof(AppDbContext)));
});
builder.Services.AddHostedService<BotBackgroundService>();
builder.Services.AddRefitClient<IFileService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(userDbUrl));
builder.Services.AddSingleton<AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.


app.Run();

