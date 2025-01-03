namespace Ali.Delivery.Order.Application.Abstractions;

public interface IPermissionService
{
    bool IsValidPermission(string permissionName);
}

public class PermissionService : IPermissionService
{
    private readonly List<string> _validPermissions = new()
    {
        "CreateOrder",
        "DeleteOrder",
        "UpdateOrder",
        "TrackOrder",
        // Добавьте другие пермишены
    };

    public bool IsValidPermission(string permissionName)
    {
        return _validPermissions.Contains(permissionName);
    }
}

