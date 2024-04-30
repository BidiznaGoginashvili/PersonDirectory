using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonDirectory.Domain.PersonManagement.ReadModels;
using PersonDirectory.Domain.PersonManagement.ValueObjects;

namespace PersonDirectory.Infrastructure.Database.Configurations
{
    public class PersonReadModelConfiguration : IEntityTypeConfiguration<PersonReadModel>
    {
        public void Configure(EntityTypeBuilder<PersonReadModel> builder)
        {
            builder.Property(person => person.LastName)
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(person => person.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(person => person.IdentificationNumber)
                .HasMaxLength(12)
                .IsRequired();

            builder.Property(person => person.PhoneNumbers)
                   .HasConversion(
                       v => JsonConvert.SerializeObject(v),
                       v => JsonConvert.DeserializeObject<IEnumerable<PhoneNumber>>(v)
                   );

            builder.ToTable(nameof(PersonReadModel));
        }
    }
}
