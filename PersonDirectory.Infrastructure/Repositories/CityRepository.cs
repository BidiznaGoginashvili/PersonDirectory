using PersonDirectory.Domain.CityManagement;
using PersonDirectory.Infrastructure.Database;
using PersonDirectory.Domain.CityManagement.Repository;

namespace PersonDirectory.Infrastructure.Repositories
{
    public class CityRepository : BaseRepository<DatabaseContext, City>, ICityRepository
    {
        DatabaseContext _context;

        public CityRepository(DatabaseContext context) : base(context) =>
            _context = context;
    }
}
