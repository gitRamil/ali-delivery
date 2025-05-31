namespace Ali.Delivery.Location.Infrastructure.Interfaces;

public interface ILocationService
{
    Task<bool> SaveLocationAsync(long userId, string userLogin, double latitude, double longitude);
}
