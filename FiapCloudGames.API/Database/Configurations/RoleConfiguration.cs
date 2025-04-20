using FiapCloudGames.API.Entities;
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
                new Role { Name = AppRoles.Admin},
                new Role { Name = AppRoles.User }
            );
        }
    }
}
