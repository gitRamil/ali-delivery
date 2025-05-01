namespace Ali.Delivery.Location.Infrastructure.Services.WriteToDataBased;

public interface IWriteToDatabase
{
    // Task<bool> CheckUserExists(string login, string password);
    Task<bool> CreateUserLocationIfNotExists(string telegramLogin);
    public Task SetCoordinates(string userName, string? E, string? S);
}