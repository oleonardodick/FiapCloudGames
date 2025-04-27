using System.ComponentModel.DataAnnotations;

namespace FiapCloudGames.API.DTOs.Requests.UserDTO
{
    public class RequestCreateUserDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public Guid RoleId { get; set; }
    }
}
