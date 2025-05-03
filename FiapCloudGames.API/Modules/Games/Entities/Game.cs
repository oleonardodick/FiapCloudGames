using FiapCloudGames.API.Shared.Entities;

namespace FiapCloudGames.API.Modules.Games.Entities
{
    public class Game : BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
    }
}
