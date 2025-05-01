using System.Text.Json.Serialization;

namespace Ali.Delivery.Location.Infrastructure.ExternalServices.Models;

public class UserInfo
{
    [JsonPropertyName("id")] public Guid Id { get; set; }

    [JsonPropertyName("login")] public string Login { get; set; }

    [JsonPropertyName("firstName")] public string FirstName { get; set; }

    [JsonPropertyName("lastName")] public string LastName { get; set; }

    [JsonPropertyName("passportInfoPassportNumber")]
    public string PassportInfoPassportNumber { get; set; }

    [JsonPropertyName("passportType")] public string PassportType { get; set; }

    [JsonPropertyName("role")] public string Role { get; set; }

    [JsonPropertyName("regDate")] public DateTime RegDate { get; set; }

    [JsonPropertyName("issuedBy")] public string IssuedBy { get; set; }
}