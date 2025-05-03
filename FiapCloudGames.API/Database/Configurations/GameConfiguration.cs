using FiapCloudGames.API.Modules.Games.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiapCloudGames.API.Database.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Description).HasMaxLength(500);
            builder.Property(u => u.Price).IsRequired().HasPrecision(8,2);
            builder.Property(u => u.CreatedAt).IsRequired();
        }
    }
}
