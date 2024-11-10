using System.Reflection;
using Ali.Delivery.Order.Application.Behaviors;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ali.Delivery.Order.WebApi.Options;

/// <summary>
/// Представляет конфигурацию для <see cref="SwaggerGenOptions" />.
/// </summary>
internal sealed class SwaggerGenOptionsConfigure : IConfigureOptions<SwaggerGenOptions>
{
    /// <inheritdoc />
    public void Configure(SwaggerGenOptions swaggerGenOptions)
    {
        swaggerGenOptions.SupportNonNullableReferenceTypes();
        swaggerGenOptions.UseAllOfForInheritance();
        swaggerGenOptions.UseOneOfForPolymorphism();

        swaggerGenOptions.SwaggerDoc("v1",
                                     new OpenApiInfo
                                     {
                                         Title = "Ali.Delivery.Order API",
                                         Description = "Сервис заказов",
                                         Version = "v1"
                                     });
        AddXmlComments(swaggerGenOptions);
    }

    private static void AddXmlComments(SwaggerGenOptions options)
    {
        var webApiXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var webApiXmlPath = Path.Combine(AppContext.BaseDirectory, webApiXmlFile);
        var applicationXmlFile = $"{typeof(RequestValidationBehaviour<,>).Assembly.GetName().Name}.xml";
        var applicationXmlPath = Path.Combine(AppContext.BaseDirectory, applicationXmlFile);
        options.IncludeXmlComments(webApiXmlPath, true);
        options.IncludeXmlComments(applicationXmlPath);
    }
}