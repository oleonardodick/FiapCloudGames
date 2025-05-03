using FiapCloudGames.API.Modules.Roles.Entities;
using FiapCloudGames.API.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiapCloudGames.API.Database.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(r => r.Name).IsRequired().HasMaxLength(20);
            builder.Property(r => r.CreatedAt).IsRequired();

            builder.HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Role { Id = Guid.Parse("1bd7d258-b3fd-4e95-985f-811173518b30"), Name = AppRoles.Admin},
                new Role { Id = Guid.Parse("93f46947-3ff4-4ed2-a3eb-4fcec16014eb"), Name = AppRoles.User }
            );
        }
    }
}
