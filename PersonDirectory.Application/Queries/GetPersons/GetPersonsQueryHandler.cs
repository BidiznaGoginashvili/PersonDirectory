using Microsoft.EntityFrameworkCore;
using PersonDirectory.Shared.Infrastructure;
using PersonDirectory.Infrastructure.Database;
using PersonDirectory.Application.Queries.ReadModels;
using PersonDirectory.Domain.PersonManagement.ReadModels;

namespace PersonDirectory.Application.Queries.GetPersons
{
    public class GetPersonsQueryHandler : BaseQueryHandler<GetPersonsQuery, IEnumerable<PersonDto>>
    {
        private DatabaseContext _context;
        public GetPersonsQueryHandler(DatabaseContext context) =>
            _context = context;

        public override async Task<BaseQueryResult<IEnumerable<PersonDto>>> Handle(GetPersonsQuery query, CancellationToken cancellationToken)
        {
            var persons = _context.Set<PersonReadModel>()
                                  .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.LastName))
                persons = persons.Where(person => person.LastName.Contains(query.LastName));

            if (!string.IsNullOrWhiteSpace(query.FirstName))
                persons = persons.Where(person => person.FirstName.Contains(query.FirstName));

            if (!string.IsNullOrWhiteSpace(query.PhoneNumber))
                persons = persons.Where(person => person.PhoneNumbers.Any(phone => phone.Number.Contains(query.PhoneNumber)));

            if (!string.IsNullOrWhiteSpace(query.IdentificationNumber))
                persons = persons.Where(person => person.IdentificationNumber == query.IdentificationNumber);

            if (query.BirthDateFrom.HasValue)
                persons = persons.Where(person => person.BirthDate >= query.BirthDateFrom);

            if (query.BirthDateTo.HasValue)
                persons = persons.Where(person => person.BirthDate <= query.BirthDateTo);

            var result = await persons
                .Select(person => new PersonDto(
                    person.Id,
                    person.CityId,
                    person.City,
                    person.Gender,
                    person.LastName,
                    person.FirstName,
                    person.PhotoPath,
                    person.BirthDate,
                    person.IdentificationNumber
                )).Skip(query.PageNumber * query.PageSize)
                  .Take(query.PageNumber)
                .ToListAsync(cancellationToken);

            return await OkAsync(result);
        }
    }
}
