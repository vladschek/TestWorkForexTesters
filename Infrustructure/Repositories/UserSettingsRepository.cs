using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.Interfaces.Repositories.Mongo;
using Microsoft.AspNetCore.Builder;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure.Data.Mongo.Repositories
{
    public class UserSettingsRepository : MongoRepository<UserSettings>, IUserSettingRepository
    {
        public UserSettingsRepository(string connectionString, string databaseName, string collectionName) : base(connectionString, databaseName, collectionName)
        {
        }

        public async Task CreateAsync(int userId, UserSettings entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task<bool> DeleteAsync(int userId)
        {
            var userIdFilter = Builders<UserSettings>.Filter.Eq(p => p.UserId, userId);
            var result = await _collection.DeleteManyAsync(userIdFilter);
            return result.DeletedCount > 0;
        }

        public async Task<UserSettings> GetForUser(int userId)
        {
            var userIdFilter = Builders<UserSettings>.Filter.Eq("UserId", userId);
            return await _collection.Find(userIdFilter).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(int id, UserSettings entity)
        {
            var userIdFilter = Builders<UserSettings>.Filter.Eq("UserId", id);
            var result = await _collection.ReplaceOneAsync(userIdFilter, entity);
            return result.ModifiedCount > 0;
        }

        public static async Task SeedDataAsync(IUserSettingRepository userSettingRepository)
        {
            if (!await userSettingRepository.DataExist())
            {
                var userSettingsList = new List<UserSettings>
                {
                    new UserSettings { UserId = 1, Language = Language.English, Theme = Theme.Light },
                    new UserSettings { UserId = 2, Language = Language.Spanish, Theme = Theme.Dark },
                    new UserSettings { UserId = 3, Language = Language.English, Theme = Theme.Light },
                    new UserSettings { UserId = 4, Language = Language.Spanish, Theme = Theme.Dark },
                    new UserSettings { UserId = 5, Language = Language.English, Theme = Theme.Light },
                };

                foreach (var us in userSettingsList)
                {
                    await userSettingRepository.CreateAsync(us.UserId, us);
                }
            }
        }

        public async Task<bool> DataExist()
        {
            var count = await _collection.CountDocumentsAsync(FilterDefinition<UserSettings>.Empty);
            return  count > 0;
        }
    }
}
