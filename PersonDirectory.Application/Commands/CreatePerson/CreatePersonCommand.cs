using PersonDirectory.Shared.Infrastructure;
using PersonDirectory.Domain.PersonManagement.Enums;
using PersonDirectory.Domain.PersonManagement.ValueObjects;

namespace PersonDirectory.Application.Commands.CreatePerson
{
    public class CreatePersonCommand : BaseCommand
    {
        public int CityId { get; set; }
        public Gender Gender { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime BirthDate { get; set; }
        public string IdentificationNumber { get; set; }
        public IEnumerable<PhoneNumber>? PhoneNumbers { get; set; }
    }
}
