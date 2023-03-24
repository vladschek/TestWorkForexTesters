using Users.API.Profiles;

namespace Users.API.Extensions
{
    public static class ConfigureAutoMapperExtension
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(options => options.AddProfile<MappingProfile>());
        }
    }
}
