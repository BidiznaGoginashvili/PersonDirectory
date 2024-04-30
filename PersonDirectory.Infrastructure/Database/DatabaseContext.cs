using Microsoft.EntityFrameworkCore;
using PersonDirectory.Domain.CityManagement;
using PersonDirectory.Infrastructure.Database.Configurations;

namespace PersonDirectory.Infrastructure.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CityConfiguration());
            builder.ApplyConfiguration(new PersonConfiguration());
            builder.ApplyConfiguration(new RelatedPersonConfiguration());
            builder.ApplyConfiguration(new PersonReadModelConfiguration());

            SeedData(builder);

            base.OnModelCreating(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            var entityType = builder.Entity<City>().Metadata;

            if (!entityType.GetSeedData().Any())
            {
                var city = new City("Tbilisi");
                city.SetId(1);
                builder.Entity<City>().HasData(
                    city
                );
            }
        }
    }
}
