using System.Text;
using Ali.Delivery.Order.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Ali.Delivery.Order.WebApi.IoC;

/// <summary>
/// 
/// </summary>
public static class AuthExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="jwtOptions"></param>
    public static void AddApiAuthentication(this IServiceCollection services, IOptions<JwtOptions> jwtOptions)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                              options =>
                              {
                                  options.TokenValidationParameters = new TokenValidationParameters
                                  {
                                      ValidateIssuer = false,
                                      ValidateAudience = false,
                                      ValidateLifetime = true,
                                      ValidateIssuerSigningKey = true,
                                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey))
                                  };

                                  options.Events = new JwtBearerEvents
                                  {
                                      OnMessageReceived = context =>
                                      {
                                          context.Token = context.Request.Cookies["token"];
                                          return Task.CompletedTask;
                                      }
                                  };
                              });
    }
}
