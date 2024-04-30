using System.Collections.ObjectModel;

namespace PersonDirectory.Shared.DomainModels
{
    public class AggregateRoot : Entity, IDomainEventPublisher
    {
        private IList<DomainEvent> _events { get; } = new List<DomainEvent>();

        IReadOnlyList<DomainEvent> IDomainEventPublisher.UncommittedChanges() =>
            new ReadOnlyCollection<DomainEvent>(_events);

        public void MarkChangesAsCommitted() =>
            _events.Clear();

        void IDomainEventPublisher.Raise(DomainEvent evnt) =>
            _events.Add(evnt);

        protected void Raise(DomainEvent @event)
        {
            var type = @event.GetType().Name;

            @event.ChangeDetails(type,
                                 Id,
                                 DateTime.UtcNow);

            (this as IDomainEventPublisher).Raise(@event);
        }
    }
}
