namespace Users.API.Extensions
{
    public static class ConfigureHttpClientExtension
    {
        public static void ConfigureHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient("ProjectService", client =>
            {
                client.BaseAddress = new Uri("http://projects.api:5001");
            });
        }
    }
}
