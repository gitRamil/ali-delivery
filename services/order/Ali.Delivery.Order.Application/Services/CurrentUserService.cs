using System.Security.Claims;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Microsoft.AspNetCore.Http;

namespace Ali.Delivery.Order.Application.Services;

/// <inheritdoc />
public class CurrentUserService : ICurrentUser
{
    private readonly ClaimsPrincipal _user;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _user = httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();

        var userIdClaim = _user.Claims.FirstOrDefault(c => c.Type == "userId");
        if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var id))
        {
            Id = id;
        }

        IsAuthenticated = _user.Identity?.IsAuthenticated ?? false;
    }

    /// <inheritdoc />
    public Guid Id { get; }

    /// <inheritdoc />
    public bool IsAuthenticated { get; }

    /// <inheritdoc />
    public bool HasPermission(params Permission[] permissions)
    {
        if (!IsAuthenticated)
        {
            return false;
        }

        var userPermissions = _user.Claims
                                   .Where(c => c.Type == "userPermissions")
                                   .Select(c => c.Value)
                                   .ToList();
            
        return permissions.Any(permission => userPermissions.Contains(permission.Code));
    }
}