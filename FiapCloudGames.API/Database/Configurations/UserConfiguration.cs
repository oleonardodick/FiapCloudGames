using FiapCloudGames.API.Modules.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiapCloudGames.API.Database.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(100);
            builder.Property(u => u.CreatedAt).IsRequired();

            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new User
                {
                    Id = Guid.Parse("440f02c7-1a65-4d78-a688-2d761373bad1"),
                    Name = "Admin",
                    Email = "admin@fcg.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("@Admin1234"),
                    RoleId = Guid.Parse("1bd7d258-b3fd-4e95-985f-811173518b30")
                }
            );
        }
    }
}
