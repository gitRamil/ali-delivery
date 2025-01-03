using System.Security.Claims;
using Ali.Delivery.Order.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.WebApi.Attribute;

public class PermissionAttribute : System.Attribute, IAuthorizationFilter
{
    private readonly string _permission;
    
    public PermissionAttribute(string permission)
    {
        _permission = permission;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var dbContext = context.HttpContext.RequestServices.GetService<IAppDbContext>();
        var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier); // или другой Claim
        
        if (userId == null)
        {
            context.Result = new ForbidResult();
            return;
        }

        var roleId = context.HttpContext.User.FindFirst("roleId");
        if (roleId == null)
        {
            context.Result = new ForbidResult();
            return;
        }

        var roleIdValue = roleId.Value;

        var hasPermission = dbContext != null && dbContext.RolePermissions
                                                          .AsQueryable() // Уточняем тип
                                                          .AsNoTracking()
                                                          .Any(r => r.Role.Id.ToString() == roleIdValue && r.Permission.Code == _permission);


        if (!hasPermission)
        {
            context.Result = new ForbidResult();
        }
    }
}