using System.Text.Json.Serialization;

namespace FiapCloudGames.API.Modules.Users.DTOs.Responses
{
    public class ResponseUserDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? AccessToken { get; set; }
    }
}
