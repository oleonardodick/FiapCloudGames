namespace FiapCloudGames.API.Modules.Roles.DTOs.Responses
{
    public class ResponseRoleDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public required string Name { get; set; }
    }
}
