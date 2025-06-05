namespace Ali.Delivery.Location.Infrastructure.Interfaces;

public interface IUserDataManager
{
    void ClearUserData(long userId);

    Task<string?> GetUserLoginAsync(long userId);

    void SaveUserData(long userId, Dictionary<string, object> userData);
}
