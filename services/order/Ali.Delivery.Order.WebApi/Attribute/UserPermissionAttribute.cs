using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ali.Delivery.Order.WebApi.Attribute;

/// <summary>
/// </summary>
/// <param name="permissions"></param>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class UserPermissionAttribute(params string[] permissions) : System.Attribute, IAsyncActionFilter
{
    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
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

        var userPermission = jwtToken.Claims.Where(c => c.Type == "userPermissions").Select(c=> c.Value)
                                     .ToList();

        if (!permissions.Any(permission => userPermission.Contains(permission)))
        {
            context.Result = new ForbidResult();
            return;
        }

        await next();
    }
}
