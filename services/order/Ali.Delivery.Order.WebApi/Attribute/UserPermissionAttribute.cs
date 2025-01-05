using System.IdentityModel.Tokens.Jwt;
using Ali.Delivery.Order.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ali.Delivery.Order.WebApi.Attribute;

/// <summary>
/// Атрибут для проверки разрешений пользователя на основе предоставленных кодов разрешений.
/// </summary>
/// <param name="permissions">Массив кодов разрешений пользователя.</param>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class UserPermissionAttribute(params UserPermissionCode[] permissions) : System.Attribute, IAsyncActionFilter
{
    /// <summary>
    /// Асинхронно выполняет проверку разрешений пользователя перед выполнением действия.
    /// </summary>
    /// <param name="context">Контекст выполнения действия.</param>
    /// <param name="next">Делегат для продолжения выполнения действия.</param>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var tokenString = context.HttpContext.Request.Cookies["token"];

        if (tokenString == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var tokenHandler = new JwtSecurityTokenHandler();

        if (!tokenHandler.CanReadToken(tokenString))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var jwtToken = tokenHandler.ReadJwtToken(tokenString);

        var userPermission = jwtToken.Claims.Where(c => c.Type == "userPermissions")
                                     .Select(c => c.Value)
                                     .ToList();

        if (!permissions.Any(permission => userPermission.Contains(((int)permission).ToString())))
        {
            context.Result = new ForbidResult();
            return;
        }

        await next();
    }
}
