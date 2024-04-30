using PersonDirectory.Shared.Infrastructure;
using PersonDirectory.Domain.PersonManagement.Enums;
using PersonDirectory.Domain.PersonManagement.ValueObjects;

namespace PersonDirectory.Application.Commands.ChangePerson
{
    public class ChangePersonCommand : BaseCommand
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public Gender Gender { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhotoPath { get; set; }
        public DateTime BirthDate { get; set; }
        public string IdentificationNumber { get; set; }
        public IEnumerable<PhoneNumber>? PhoneNumbers { get; set; }
    }
}
