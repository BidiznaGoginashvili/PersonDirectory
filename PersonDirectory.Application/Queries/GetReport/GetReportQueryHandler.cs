using Microsoft.EntityFrameworkCore;
using PersonDirectory.Shared.Infrastructure;
using PersonDirectory.Application.Queries.Dtos;
using PersonDirectory.Domain.PersonManagement.Enums;
using PersonDirectory.Domain.PersonManagement.Repository;

namespace PersonDirectory.Application.Queries.GetReport
{
    public class GetReportQueryHandler : BaseQueryHandler<GetReportQuery, IEnumerable<PersonReportDto>>
    {
        private readonly IPersonRepository _personRepository;
        public GetReportQueryHandler(IPersonRepository personRepository) =>
            _personRepository = personRepository;

        public override async Task<BaseQueryResult<IEnumerable<PersonReportDto>>> Handle(GetReportQuery query, CancellationToken cancellationToken)
        {
            var person = await _personRepository.Query(person => person.Id == query.PersonId)
                                                .Include(person => person.RelatedPersons)
                                                .SingleOrDefaultAsync();

            if (person == null)
                return await FailAsync(ErrorCode.NotFound);

            var report = person.RelatedPersons
                .GroupBy(relatedPerson => relatedPerson.RelationshipType)
                .Select(g => new PersonReportDto
                {
                    Name = person.FirstName,
                    RelatedPersonsCount = g.Count(),
                    RelationshipType = ((RelationshipType)g.Key).ToString()
                })
                .ToList();

            return await OkAsync(report);
        }
    }
}
