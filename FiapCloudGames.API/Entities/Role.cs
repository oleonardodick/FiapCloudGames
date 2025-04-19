﻿namespace FiapCloudGames.API.Entities
{
    public class Role : BaseEntity
    {
        public required string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
