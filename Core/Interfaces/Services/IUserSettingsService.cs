using Common.DTOs;

namespace Core.Interfaces.Services
{
    public interface IUserSettingsService
    {
        Task<UserSettingsDTO> GetForUser(int userId);
        Task CreateAsync(int userId, UserSettingsDTO entity);
        Task<bool> UpdateAsync(int id, UserSettingsDTO entity);
        Task<bool> DeleteAsync(int userId);
    }
}
