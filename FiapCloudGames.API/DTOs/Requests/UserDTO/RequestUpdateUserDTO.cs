namespace FiapCloudGames.API.DTOs.Requests.UserDTO
{
    public class RequestUpdateUserDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public Guid? RoleId { get; set; }
    }
}
