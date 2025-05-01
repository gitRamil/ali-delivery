using Ali.Delivery.Location.Infrastructure.ExternalServices;
using Ali.Delivery.Location.Infrastructure.Services.MyConfiguration;
using Npgsql;

namespace Ali.Delivery.Location.Infrastructure.Services.WriteToDataBased;

public class WriteToDatabase : IWriteToDatabase
{
    private readonly IMyConfigurationService _configurationService;
    private readonly IFileService _baseService;

    public WriteToDatabase(IMyConfigurationService configurationService) => _configurationService = configurationService;

    
    // public async Task<bool> CheckUserExists(string login, string password)
    // {
    //     _baseService.PostLogin(login,password)
    //     // await using var conn = new NpgsqlConnection(_configurationService.GetDatabaseConnectionString());
    //     // await conn.OpenAsync();
    //     // // Добавляем @ как в методе SetCoordinates
    //     // var telegramLogin = login.Trim();
    //     //
    //     // var cmd = new NpgsqlCommand(
    //     //     @"SELECT * FROM users WHERE login LIKE @telegramLogin;", 
    //     //     conn);
    //     //
    //     // cmd.Parameters.AddWithValue("telegramLogin", telegramLogin);
    //     // return await cmd.ExecuteScalarAsync() != null;
    // }

    public async Task<bool> CreateUserLocationIfNotExists(string telegramLogin)
    {
        try
        {
            await using var conn = new NpgsqlConnection(_configurationService.GetConnectionString());
            await conn.OpenAsync();

            // // Проверяем существование пользователя
            // var checkCmd = new NpgsqlCommand(
            //     @"SELECT * FROM ""userLocations"" WHERE ""TelegramLogin"" LIKE @telegramLogin", 
            //     conn);
            // checkCmd.Parameters.AddWithValue("TelegramLogin", telegramLogin);
            //
            // if (await checkCmd.ExecuteScalarAsync() == null)
            // {
            //     return false;
            // }

            // Вставляем новую запись с генерацией UUID
            var insertCmd = new NpgsqlCommand(
                @"INSERT INTO ""userLocations"" (""Id"", ""TelegramLogin"") VALUES (gen_random_uuid(), @telegramLogin)", 
                conn);
        
            insertCmd.Parameters.AddWithValue("telegramLogin", telegramLogin);
            return await insertCmd.ExecuteNonQueryAsync() > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating user location: {ex.Message}");
            return false;
        }
    }
    public async Task SetCoordinates(string telegramLogin, string? e, string? s)
    {
        // Существующая реализация
        if (string.IsNullOrWhiteSpace(e) || string.IsNullOrWhiteSpace(s)) return;
        
        await using var conn = new NpgsqlConnection(_configurationService.GetConnectionString());
        await conn.OpenAsync();
        var cmd = new NpgsqlCommand(@"SELECT ""Id"" FROM ""userLocations"" WHERE ""TelegramLogin"" LIKE @telegramLogin", conn);
        cmd.Parameters.AddWithValue("TelegramLogin", telegramLogin);
        var id = (Guid?)await cmd.ExecuteScalarAsync();

        if (id == null)
        {
            return; 
        }

        cmd = new NpgsqlCommand(@"UPDATE ""userLocations"" SET ""E""=@e, ""S""=@s WHERE ""Id"" = @id", conn);
        cmd.Parameters.AddWithValue("e", e);
        cmd.Parameters.AddWithValue("s", s);
        cmd.Parameters.AddWithValue("id", id);
        await cmd.ExecuteNonQueryAsync();
        await conn.CloseAsync();
    }
    }

