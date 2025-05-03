using System.ComponentModel.DataAnnotations;

namespace FiapCloudGames.API.Modules.Users.DTOs.Requests
{
    public class RequestCreateUserDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public Guid RoleId { get; set; }
    }
}
