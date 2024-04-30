using PersonDirectory.Shared.Infrastructure;
using PersonDirectory.Application.Queries.ReadModels;

namespace PersonDirectory.Application.Queries.GetPerson
{
    public class GetPersonQuery : BaseQuery<PersonDto>
    {
        public int Id { get; set; } 
    }
}
