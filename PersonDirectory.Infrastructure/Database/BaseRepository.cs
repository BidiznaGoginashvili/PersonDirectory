using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PersonDirectory.Shared.DomainModels;

namespace PersonDirectory.Infrastructure.Database
{
    public class BaseRepository<TContext, TAggregateRoot> : IRepository<TAggregateRoot>
        where TContext : DbContext
        where TAggregateRoot : AggregateRoot
    {
        TContext _context;

        public BaseRepository(TContext context) =>
            _context = context;

        public async Task<TAggregateRoot?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
            await _context.Set<TAggregateRoot>()
                          .FindAsync(id, cancellationToken);

        public async Task AddAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken)
        {
            await _context.Set<TAggregateRoot>()
                          .AddAsync(aggregateRoot, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken)
        {
            _context.Entry(aggregateRoot).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken)
        {
            _context.Set<TAggregateRoot>()
                    .Remove(aggregateRoot);
        }

        public IQueryable<TAggregateRoot> Query(Expression<Func<TAggregateRoot, bool>>? expression)
            => expression == null ? _context.Set<TAggregateRoot>()
                                            .AsQueryable() :
                                    _context.Set<TAggregateRoot>()
                                            .Where(expression);
    }
}
