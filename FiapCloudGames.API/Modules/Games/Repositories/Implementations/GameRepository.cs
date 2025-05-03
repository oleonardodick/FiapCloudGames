using FiapCloudGames.API.Database;
using FiapCloudGames.API.Modules.Games.Entities;
using FiapCloudGames.API.Modules.Games.Repositories.Interfaces;
using FiapCloudGames.API.Shared.Repositories.Implementations;

namespace FiapCloudGames.API.Modules.Games.Repositories.Implementations
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository(ApplicationDbContext context) : base(context) { }
    }
}
