using Common.DTOs;
using Common.DTOs.Subscription;

namespace Core.Interfaces.Services;

public interface IUserService
{
    Task<IEnumerable<ReadUserDTO>> GetAllUsers();
    Task<ReadUserDTO> GetUserById(int Id);
    Task UpdateUser(int id, UpdateUserDTO user);
    Task DeleteUser(int Id);
    Task<int> CraeteUser(UserCreateDTO user);
    Task UpdateUserSubscription(int id, SubscriptionDTO updateSubscriptionDto);
    Task IsUserExist(int userId);
    Task<IEnumerable<int>> GetUsersBySubscription(string subscriptionType);
}
