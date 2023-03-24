using Core.Interfaces.Repositories.Postgre;
using Microsoft.EntityFrameworkCore;

namespace Infrustructure.Data.Postgre.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        protected readonly AppDbContext _context;

        protected BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false)
        {
            return trackChanges ? await _context.Set<TEntity>().ToListAsync() : await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);

            if (entity == null)
            {
                return;
            }

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
