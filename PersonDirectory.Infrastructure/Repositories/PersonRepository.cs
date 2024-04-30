using PersonDirectory.Domain.PersonManagement;
using PersonDirectory.Infrastructure.Database;
using PersonDirectory.Domain.PersonManagement.Repository;

namespace PersonDirectory.Infrastructure.Repositories
{
    public class PersonRepository : BaseRepository<DatabaseContext, Person>, IPersonRepository
    {
        DatabaseContext _context;

        public PersonRepository(DatabaseContext context) : base(context) =>
            _context = context;
    }
}
