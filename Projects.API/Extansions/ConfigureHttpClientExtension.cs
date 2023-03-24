namespace Projects.API.Extansions
{
    public static class ConfigureHttpClientExtension
    {
        public static void ConfigureHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient("UserService", client =>
            {
                client.BaseAddress = new Uri("http://users.api:5000");
            });
        }
    }
}
