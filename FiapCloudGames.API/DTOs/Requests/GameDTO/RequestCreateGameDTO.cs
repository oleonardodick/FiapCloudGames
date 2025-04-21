namespace FiapCloudGames.API.DTOs.Requests.GameDTO
{
    public class RequestCreateGameDTO
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
    }
}
