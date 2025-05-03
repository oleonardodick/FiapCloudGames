using FiapCloudGames.API.Modules.Games.Entities;
using FiapCloudGames.API.Modules.Roles.Entities;
using FiapCloudGames.API.Modules.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.API.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Game> Games { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
