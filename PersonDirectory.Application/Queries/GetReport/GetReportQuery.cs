using PersonDirectory.Shared.Infrastructure;
using PersonDirectory.Application.Queries.Dtos;

namespace PersonDirectory.Application.Queries.GetReport
{
    public class GetReportQuery : BaseQuery<IEnumerable<PersonReportDto>>
    {
        public int PersonId { get; set; }
    }
}
