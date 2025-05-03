using FiapCloudGames.API.Modules.Users.Entities;
using FiapCloudGames.API.Shared.Entities;

namespace FiapCloudGames.API.Modules.Roles.Entities
{
    public class Role : BaseEntity
    {
        public required string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
