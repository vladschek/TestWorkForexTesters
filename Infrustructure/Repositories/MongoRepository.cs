using Core.Interfaces.Repositories.Mongo;
using MongoDB.Driver;

namespace Infrustructure.Data.Mongo.Repositories
{
    public class MongoRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly IMongoCollection<T> _collection;

        public MongoRepository(string connectionString, string databaseName, string collectionName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var filter = Builders<T>.Filter.Eq("UserId", id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            var filter = Builders<T>.Filter.Eq("UserId", entity.GetType().GetProperty("UserId").GetValue(entity));
            var result = await _collection.ReplaceOneAsync(filter, entity);
            return result.ModifiedCount > 0;
        }
    }
}
