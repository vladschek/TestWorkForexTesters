using Core.Interfaces.Repositories.Mongo;
using Infrustructure.Data.Mongo.Repositories;

namespace Projects.API.Extansions
{
    public static class ConfigureRepositoriesExtension
    {
        public static void ConfigureRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IProjectsRepository>(sp => new ProjectsRepository(
                configuration.GetConnectionString("MongoDb"),
                configuration["MongoDbSettings:DatabaseName"],
                configuration["MongoDbSettings:ProjectsCollectionName"]
            ));
            services.AddSingleton<IUserSettingRepository>(sp => new UserSettingsRepository(
                configuration.GetConnectionString("MongoDb"),
                configuration["MongoDbSettings:DatabaseName"],
                configuration["MongoDbSettings:UserSettingsCollectionName"]
            ));
        }
    }
}
