using Core.Domain.Entities;
using Core.Domain.Enums;

namespace Core.Interfaces.Repositories.Postgre
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync(bool trackChanges = false);
        Task<User> GetById(int id, bool trackChanges = false);
        Task<User> UpdateAsync(User entity);
        Task DeleteAsync(int id);
        Task Create(User user);
        Task<int> SaveChangeAsync();
        Task<IEnumerable<int>> GetUsersWithSubscription(SubscriptionType subscriptionType);
    }
}
