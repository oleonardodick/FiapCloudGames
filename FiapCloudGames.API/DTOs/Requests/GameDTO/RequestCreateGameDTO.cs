using System.ComponentModel.DataAnnotations;

namespace FiapCloudGames.API.DTOs.Requests.GameDTO
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
