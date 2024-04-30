using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using PersonDirectory.Domain.PersonManagement;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonDirectory.Domain.PersonManagement.ValueObjects;

namespace PersonDirectory.Infrastructure.Database.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
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

            builder.HasMany(person => person.RelatedPersons)
                .WithOne()
                .HasForeignKey(relatedPerson => relatedPerson.PersonId);

            builder.ToTable(nameof(Person));
        }
    }
}
