using PersonDirectory.Domain.PersonManagement.Enums;
using PersonDirectory.Domain.PersonManagement.ValueObjects;

namespace PersonDirectory.Domain.PersonManagement.ReadModels
{
    public class PersonReadModel
    {
        public PersonReadModel() { }

        public PersonReadModel(string city,
                               Person person)
        {
            City = city;
            PersonId = person.Id;
            CityId = person.CityId;
            Gender = person.Gender;
            LastName = person.LastName;
            FirstName = person.FirstName;
            PhotoPath = person.PhotoPath;
            BirthDate = person.BirthDate;
            PhoneNumbers = person.PhoneNumbers;
            IdentificationNumber = person.IdentificationNumber;
        }

        public int Id { get; private set; }
        public int PersonId { get; private set; }
        public int? CityId { get; private set; }
        public string? City { get; private set; }
        public Gender Gender { get; private set; }
        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public string? PhotoPath { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string IdentificationNumber { get; private set; }
        public IEnumerable<PhoneNumber>? PhoneNumbers { get; private set; }

        public void ChangeDetails(string city, Person person)
        {
            City = city;
            Gender = person.Gender;
            CityId = person.CityId;
            LastName = person.LastName;
            FirstName = person.FirstName;
            PhotoPath = person.PhotoPath;
            BirthDate = person.BirthDate;
            IdentificationNumber = person.IdentificationNumber;
            PhoneNumbers = person.PhoneNumbers;
        }
    }
}
