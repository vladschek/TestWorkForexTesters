using Projects.API.Profiles;

namespace Projects.API.Extansions
{
    public static class ConfigureAutoMapperExtension
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(options => options.AddProfile<MappingProfile>());
        }
    }
}
