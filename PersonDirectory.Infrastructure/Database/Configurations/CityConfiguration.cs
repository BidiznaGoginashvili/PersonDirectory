using Microsoft.EntityFrameworkCore;
using PersonDirectory.Domain.CityManagement;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonDirectory.Infrastructure.Database.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.Property(city => city.Name)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.ToTable(nameof(City));
        }
    }
}
