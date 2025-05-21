using System.Net;
using Ali.Delivery.Location.Infrastructure.ExternalServices;
using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;
using Refit;

namespace Ali.Delivery.Location.Infrastructure.Services.WriteToDataBased;

public class WriteToDatabase(IFileServiceForLocation serviceForLocation) : IWriteToDatabase
{
    public async Task<bool> UpsertUserLocation(string telegramLogin, string? e = null, string? s = null)
    {
        try
        {
            var dto = new LocationDto
            {
                TelegramLogin = telegramLogin,
                E = e ?? "",
                S = s ?? ""
            };

            // Сначала пытаемся обновить
            var updateResponse = await serviceForLocation.UpdateUserLocationAsync(dto);

            // Если пользователя нет (получили 404) — создаём
            if (updateResponse.StatusCode == HttpStatusCode.NotFound)
            {
                var createResponse = await serviceForLocation.CreateUserLocationAsync(dto);
                return createResponse.IsSuccessStatusCode;
            }

            // Если ошибка не 404 — возвращаем false
            if (!updateResponse.IsSuccessStatusCode)
            {
                Console.WriteLine($"Update failed: {updateResponse.StatusCode}");
                return false;
            }

            return true; // Обновление прошло успешно
        }
        catch (ApiException ex)
        {
            Console.WriteLine($"API Error: {ex.Message}");
            return false;
        }
    }
}

