using FiapCloudGames.API.Database;
using FiapCloudGames.API.Entities;
using FiapCloudGames.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.API.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected ApplicationDbContext _context;
        protected DbSet<T> _dbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task Create(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<(IList<T>, int totalItems)> GetAll(int pageNumber, int pageSize)
        {
            var query = _dbSet.AsQueryable();
            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(x => x.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
            
        }

        public async Task<T?> GetById(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
