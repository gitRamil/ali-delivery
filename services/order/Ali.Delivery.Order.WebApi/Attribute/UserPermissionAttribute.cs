using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ali.Delivery.Order.WebApi.Attribute;

/// <summary>
/// 
/// </summary>
/// <param name="permission"></param>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class UserPermissionAttribute(string permission) : System.Attribute, IAsyncActionFilter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var tokenString = context.HttpContext.Request.Cookies["tasty-cookies"];
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
        var userPermission = jwtToken.Claims.FirstOrDefault(c => c.Type == "userPermissions")?.Value;

        if (userPermission != permission)
        {
            context.Result = new ForbidResult();
            return;
        }

        await next();
    }
}