namespace PersonDirectory.Shared.Dispatcher
{
    public interface IEventDispatcher<TDomainEvent>
    {
        Task DispatchAsync(IReadOnlyList<TDomainEvent> domainEvents, CancellationToken cancellationToken);
    }
}
