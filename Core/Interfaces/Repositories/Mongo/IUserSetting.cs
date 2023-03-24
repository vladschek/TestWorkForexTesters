using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories.Mongo
{
    public interface IUserSettingRepository
    {
        Task<UserSettings> GetForUser(int userId);
        Task CreateAsync(int userId,UserSettings entity);
        Task<bool> UpdateAsync(int id, UserSettings entity);
        Task<bool> DeleteAsync(int userId);
        Task<bool> DataExist();
    }
}
