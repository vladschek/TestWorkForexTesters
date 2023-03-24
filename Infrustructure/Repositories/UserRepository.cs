using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.Interfaces.Repositories.Postgre;
using Microsoft.EntityFrameworkCore;

namespace Infrustructure.Data.Postgre.Repositories
{
    public sealed class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context, bool trackChanges = false) : base(context)
        {
        }

        public async Task Create(User user)
        {
            await CreateAsync(user);
        }

        public async Task<IEnumerable<User>> GetAllAsync(bool trackChanges = false)
        {
            var query = _context.Users.Include(user => user.Subscription).AsQueryable();
            query = trackChanges ? query : query.AsNoTracking();
            return await query.ToListAsync();
        }
        public async Task<User?> GetById(int id, bool trackChanges = false)
        {
            var query = _context.Users.Include(user => user.Subscription).Where(u => u.Id == id);
            query = trackChanges ? query : query.AsNoTracking();
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<int>> GetUsersWithSubscription(SubscriptionType subscriptionType)
        {
            return await _context.Users.Include(user => user.Subscription)
                .Where(u => u.Subscription.Type == subscriptionType)
                .Select(u => u.Id)
                .ToListAsync();
        }

        public async Task<int> SaveChangeAsync()
        {
            return await SaveChangesAsync();
        }
    }
}
