using FiapCloudGames.API.Database;
using FiapCloudGames.API.Entities;
using FiapCloudGames.API.Repositories.Interfaces;

namespace FiapCloudGames.API.Repositories.Implementations
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository(ApplicationDbContext context) : base(context) { }
    }
}
