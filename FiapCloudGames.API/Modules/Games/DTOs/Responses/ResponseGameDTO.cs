namespace FiapCloudGames.API.Modules.Games.DTOs.Responses
{
    public class ResponseGameDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
    }
}
