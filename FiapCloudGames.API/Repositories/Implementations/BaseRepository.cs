using FiapCloudGames.API.Entities;
using FiapCloudGames.API.Repositories.Interfaces;

namespace FiapCloudGames.API.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        public Task<T> Create(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<(IList<T>, int totalItems)> GetAll(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
