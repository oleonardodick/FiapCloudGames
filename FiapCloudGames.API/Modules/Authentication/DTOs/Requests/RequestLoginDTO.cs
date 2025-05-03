namespace FiapCloudGames.API.Modules.Authentication.DTOs.Requests
{
    public class RequestLoginDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
