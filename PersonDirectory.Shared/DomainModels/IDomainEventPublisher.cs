namespace PersonDirectory.Shared.DomainModels
{
    public interface IDomainEventPublisher
    {
        IReadOnlyList<DomainEvent> UncommittedChanges();

        void MarkChangesAsCommitted();

        void Raise(DomainEvent @event);
    }
}
