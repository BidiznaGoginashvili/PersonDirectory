using System.Linq.Expressions;

namespace PersonDirectory.Shared.DomainModels
{
    public interface IRepository<TAggregateRoot> where TAggregateRoot : AggregateRoot   
    {
        Task AddAsync(TAggregateRoot entity, CancellationToken cancellationToken);
        Task UpdateAsync(TAggregateRoot entity, CancellationToken cancellationToken);
        Task DeleteAsync(TAggregateRoot entity, CancellationToken cancellationToken);
        Task<TAggregateRoot?> GetByIdAsync(int id, CancellationToken cancellationToken);
        IQueryable<TAggregateRoot> Query(Expression<Func<TAggregateRoot, bool>>? expression);
    }
}
