using PersonDirectory.Shared.Infrastructure;
using PersonDirectory.Application.Queries.ReadModels;

namespace PersonDirectory.Application.Queries.GetPersons
{
    public class GetPersonsQuery : BaseQuery<IEnumerable<PersonDto>>
    {
        public string? LastName { get; set; }
        public string? FirstName { get;set; }
        public string? PhoneNumber { get; set; }
        public string? IdentificationNumber { get; set; }
        public DateTime? BirthDateFrom { get; set; }
        public DateTime? BirthDateTo { get; set; }
    }
}
