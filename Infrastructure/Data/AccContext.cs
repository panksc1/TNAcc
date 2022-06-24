using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AccContext : DbContext
    {
        public AccContext(DbContextOptions<AccContext> options) : base(options)
        {
        }

        public DbSet<Entity> Entities { get; set; }
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Payable> Payables { get; set; }
        public DbSet<Receivable> Receivables { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Band> Bands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));

                    foreach (var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                    }

                }
            }
        }
    }
}