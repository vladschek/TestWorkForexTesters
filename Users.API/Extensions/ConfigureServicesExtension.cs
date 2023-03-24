using Common.Middleware;
using Core.Interfaces.Repositories.Postgre;
using Core.Interfaces.Services;
using Infrustructure.Data.Postgre.Repositories;
using Infrustructure.Services;
using Users.API.Validators;

namespace Users.API.Extensions
{
    public static class ConfigureServicesExtension
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<SubscriptionDTOValidator>();
            services.AddTransient<SubscriptionTypeValidator>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<ExceptionHandler>();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}
