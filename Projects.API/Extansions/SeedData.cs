using Core.Domain.Entities;
using Core.Interfaces.Repositories.Mongo;
using Infrustructure.Data.Mongo.Repositories;
using MongoDB.Driver;

namespace Projects.API.Extansions
{
    public static class SeedDataExtension
    {
        public static async void SeedData(this WebApplication app, IConfiguration configuration)
        {
            using (var scope = app.Services.CreateScope())
            {
                var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
                var database = client.GetDatabase(configuration["MongoDbSettings:DatabaseName"]);
                var collection = database.GetCollection<Project>(configuration["MongoDbSettings:ProjectsCollectionName"]);
                var indexKeys = Builders<Project>.IndexKeys.Combine(Builders<Project>.IndexKeys.Ascending("UserId"), Builders<Project>.IndexKeys.Ascending("Name"));
                var indexOptions = new CreateIndexOptions { Unique = true };
                var indexModel = new CreateIndexModel<Project>(indexKeys, indexOptions);

                await collection.Indexes.CreateOneAsync(indexModel);

                Console.WriteLine("Start seeding...");
                var projectsRepository = scope.ServiceProvider.GetRequiredService<IProjectsRepository>();
                await ProjectsRepository.SeedDataAsync(projectsRepository);
                var userSettingRepository = scope.ServiceProvider.GetRequiredService<IUserSettingRepository>();
                await UserSettingsRepository.SeedDataAsync(userSettingRepository);
                Console.WriteLine("End seeding.");
            }
        }
    }
}
