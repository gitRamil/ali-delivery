namespace Ali.Delivery.Location.Infrastructure.Services.WriteToDataBased;

public interface IWriteToDatabase
{
    public Task<bool> UpsertUserLocation(string userLogin, string? e, string? s);
}