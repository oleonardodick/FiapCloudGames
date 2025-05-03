using FiapCloudGames.API.Modules.Roles.Entities;
using FiapCloudGames.API.Shared.Entities;

namespace FiapCloudGames.API.Modules.Users.Entities
{
    public class User : BaseEntity
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
