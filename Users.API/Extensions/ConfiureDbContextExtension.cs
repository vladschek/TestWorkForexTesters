using Infrustructure.Data.Postgre;
using Microsoft.EntityFrameworkCore;


namespace Users.API.Extensions
{
    public static class ConfiureDbContextExtension
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")
                        , b => b.MigrationsAssembly("Users.API")));
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
    }
}
