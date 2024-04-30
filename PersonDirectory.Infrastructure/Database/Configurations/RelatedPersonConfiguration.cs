using Microsoft.EntityFrameworkCore;
using PersonDirectory.Domain.PersonManagement;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonDirectory.Infrastructure.Database.Configurations
{
    public class RelatedPersonConfiguration : IEntityTypeConfiguration<RelatedPerson>
    {
        public void Configure(EntityTypeBuilder<RelatedPerson> builder)
        {
            builder.ToTable(nameof(RelatedPerson));
        }
    }
}
