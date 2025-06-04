namespace Ali.Delivery.Location.Infrastructure.Interfaces;

public interface IUserDataManager
{
    void SaveUserData(long userId, Dictionary<string, object> userData);
    Task<string?> GetUserLoginAsync(long userId);
    void ClearUserData(long userId);
}
