using Core.Domain.Entities;
using Core.Domain.Enums;
using Infrustructure.Data.Postgre;
using Microsoft.EntityFrameworkCore;

namespace Users.API.Extensions
{
    public static class ConfigureMigrationExtension
    {
        public static void MakeMigration(this WebApplication app, IConfiguration configuration)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    Console.WriteLine("Try Migration");
                    var context = services.GetRequiredService<AppDbContext>();
                    context.Database.Migrate();
                    Seed(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Migration fail.");
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }
        }

        private static void Seed(AppDbContext context)
        {
            Console.WriteLine("Start seeding!");
            // Seed subscriptions
            if (!context.Subscriptions.Any())
            {
                context.Subscriptions.AddRange(
                    new Subscription { Type = SubscriptionType.Free, StartDate = new DateTime(2023, 1, 1), EndDate = new DateTime(2023, 12, 31) },
                    new Subscription { Type = SubscriptionType.Super, StartDate = new DateTime(2023, 1, 1), EndDate = new DateTime(2023, 1, 31) },
                    new Subscription { Type = SubscriptionType.Trial, StartDate = new DateTime(2023, 1, 1), EndDate = new DateTime(2023, 12, 31) }
                );
                context.SaveChanges();
            }

            // Seed users
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { Name = "John Doe", Email = "john.doe@example.com", SubscriptionId = 1 },
                    new User { Name = "Jane Doe", Email = "jane.doe@example.com", SubscriptionId = 2 },
                    new User { Name = "Alice", Email = "alice@example.com", SubscriptionId = 3 }
                );
                context.SaveChanges();
            }
            Console.WriteLine("End seeding!");
        }
    }
}
