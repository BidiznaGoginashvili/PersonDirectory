using PersonDirectory.Domain.PersonManagement.Enums;

namespace PersonDirectory.Application.Queries.ReadModels
{
    public class PersonDto
    {
        public PersonDto(int personId,
                         int? cityId,
                         string? city,
                         Gender gender,
                         string lastName,
                         string firstName,
                         string? photoPath,
                         DateTime birthDate,
                         string identificationNumber)
        {
            PersonId = personId;
            CityId = cityId;
            City = city;
            Gender = gender;
            LastName = lastName;
            FirstName = firstName;
            PhotoPath = photoPath;
            BirthDate = birthDate;
            IdentificationNumber = identificationNumber;
        }

        public int PersonId { get; private set; }
        public int? CityId { get; private set; }
        public string? City { get; private set; }
        public Gender Gender { get; private set; }
        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public string? PhotoPath { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string IdentificationNumber { get; private set; }
    }
}
