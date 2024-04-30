using Microsoft.EntityFrameworkCore;
using PersonDirectory.Shared.Infrastructure;
using PersonDirectory.Infrastructure.Database;
using PersonDirectory.Application.Queries.ReadModels;
using PersonDirectory.Domain.PersonManagement.ReadModels;

namespace PersonDirectory.Application.Queries.GetPerson
{
    public class GetPersonQueryHandler : BaseQueryHandler<GetPersonQuery, PersonDto>
    {
        private DatabaseContext _context;
        public GetPersonQueryHandler(DatabaseContext context) =>
            _context = context;

        public override async Task<BaseQueryResult<PersonDto>> Handle(GetPersonQuery query, CancellationToken cancellationToken)
        {

            var person = await _context.Set<PersonReadModel>()
                                       .FirstOrDefaultAsync(person => person.PersonId == query.Id);

            if (person == null)
                return await FailAsync(ErrorCode.NotFound);

            var readModel = new PersonDto(person.Id,
                                          person.CityId,
                                          person.City,
                                          person.Gender,
                                          person.LastName,
                                          person.FirstName,
                                          person.PhotoPath,
                                          person.BirthDate,
                                          person.IdentificationNumber);

            return await OkAsync(readModel);
        }
    }
}
