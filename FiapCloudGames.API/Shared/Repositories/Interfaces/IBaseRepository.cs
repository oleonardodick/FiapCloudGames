using FiapCloudGames.API.Shared.Entities;

namespace FiapCloudGames.API.Shared.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<(IList<T>, int totalItems)> GetAll(int pageNumber, int pageSize);
        Task<T?> GetById(Guid id);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
