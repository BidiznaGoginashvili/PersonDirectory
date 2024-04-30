using PersonDirectory.Shared.DomainModels;
using PersonDirectory.Domain.PersonManagement.Enums;
using PersonDirectory.Domain.PersonManagement.ValueObjects;
using PersonDirectory.Domain.PersonManagement.DomainEvents;

namespace PersonDirectory.Domain.PersonManagement
{
    public class Person : AggregateRoot
    {
        public Person() { }

        public Person(int cityId,
                      Gender gender,
                      string lastName,
                      string firstName,
                      DateTime birthDate,
                      string identificationNumber,
                      IEnumerable<PhoneNumber>? phoneNumbers)
        {
            CityId = cityId;
            Gender = gender;
            LastName = lastName;
            FirstName = firstName;
            BirthDate = birthDate;
            IdentificationNumber = identificationNumber;
            PhoneNumbers = phoneNumbers;
            CreateDate = DateTimeOffset.UtcNow;

            Raise(new PersonCreatedDomainEvent(this));
        }

        public int CityId { get; private set; }
        public Gender Gender { get; private set; }
        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public string? PhotoPath { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string IdentificationNumber { get; private set; }
        public IEnumerable<PhoneNumber>? PhoneNumbers { get; private set; }
        public ICollection<RelatedPerson>? RelatedPersons { get; private set; }

        public void Delete()
        {
            Deleted = true;
            LastChangeDate = DateTimeOffset.UtcNow;

            Raise(new PersonDeletedDomainEvent(this.Id));
        }

        public void ChangeDetails(int cityId,
                                  Gender gender,
                                  string lastName,
                                  string firstName,
                                  string photoPath,
                                  DateTime birthDate,
                                  string identificationNumber,
                                  IEnumerable<PhoneNumber>? phoneNumbers)
        {
            CityId = cityId;
            Gender = gender;
            LastName = lastName;
            FirstName = firstName;
            PhotoPath = photoPath;
            BirthDate = birthDate;
            IdentificationNumber = identificationNumber;
            PhoneNumbers = phoneNumbers;
            LastChangeDate = DateTimeOffset.UtcNow;

            Raise(new PersonChangedDomainEvent(this));
        }

        public void SetPhotoPath(string photoPath)
        {
            LastChangeDate = DateTimeOffset.UtcNow;

            PhotoPath = photoPath;

            Raise(new PersonChangedDomainEvent(this));
        }

        public void AddRelatedPerson(int relatedPersonId, RelationshipType relationshipType)
        {
            LastChangeDate = DateTimeOffset.UtcNow;

            if (RelatedPersons == null)
                RelatedPersons = new List<RelatedPerson>();

            if (!RelatedPersons.Any(relatedPerson => relatedPerson.RelatedPersonId == relatedPersonId))
                RelatedPersons.Add(new RelatedPerson(this.Id, relatedPersonId, relationshipType));
        }

        public void RemoveRelatedPerson(int relatedPersonId)
        {
            LastChangeDate = DateTimeOffset.UtcNow;

            if (RelatedPersons != null)
            {
                var relatedPerson = RelatedPersons.FirstOrDefault(relatedPerson => relatedPerson.RelatedPersonId == relatedPersonId);

                if (relatedPerson != null)
                {
                    RelatedPersons.Remove(relatedPerson);
                }
            }
        }
    }
}
