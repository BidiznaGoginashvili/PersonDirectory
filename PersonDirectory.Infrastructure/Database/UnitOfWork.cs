using PersonDirectory.Shared.Dispatcher;
using PersonDirectory.Shared.DomainModels;
using PersonDirectory.Shared.Infrastructure;

namespace PersonDirectory.Infrastructure.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private readonly IEventDispatcher<DomainEvent> _eventDispatcher;

        public UnitOfWork(DatabaseContext context,
                          IEventDispatcher<DomainEvent> eventDispatcher)
        {
            _context = context;
            _eventDispatcher = eventDispatcher;
        }

        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                var modifiedEntries = _context.ChangeTracker.Entries<IDomainEventPublisher>().ToList();

                await _context.SaveChangesAsync(cancellationToken);

                var domainEvents = modifiedEntries.SelectMany(entry => entry.Entity.UncommittedChanges())
                                                  .ToList();

                await _eventDispatcher.DispatchAsync(domainEvents, cancellationToken);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }
    }
}
