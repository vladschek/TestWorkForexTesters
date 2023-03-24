using Common.Middleware;
using Core.Interfaces.Repositories.Mongo;
using Core.Interfaces.Services;
using Infrustructure.Data.Mongo.Repositories;
using Infrustructure.Services;
using Projects.API.Services;
using Projects.API.Validator;

namespace Projects.API.Extansions
{
    public static class ConfigureServicesExtension
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddSingleton<IProjectsService, ProjectsService>();
            services.AddSingleton<IUserSettingsService, UserSettingService>();
            services.AddTransient<ExceptionHandler>();
            services.AddTransient<ChartDtoValidator>();
        }
    }
}
