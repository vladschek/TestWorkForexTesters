using Core.Domain.Entities;
using Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrustructure.Data.Postgre
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.Property(e => e.Type)
                    .HasConversion(
                    v => v.ToString(),
                    v => (SubscriptionType)Enum.Parse(typeof(SubscriptionType), v))
                    .HasMaxLength(50);
            });
        }

    }
}
