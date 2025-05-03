using System.ComponentModel.DataAnnotations;

namespace FiapCloudGames.API.Modules.Games.DTOs.Requests
{
    public class RequestCreateGameDTO
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
