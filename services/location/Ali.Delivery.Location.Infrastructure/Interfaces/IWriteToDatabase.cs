namespace Ali.Delivery.Location.Infrastructure.Interfaces;

public interface IWriteToDatabase
{
    public Task<bool> UpsertUserLocation(string userLogin, string? e, string? s);
}
