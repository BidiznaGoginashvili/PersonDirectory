namespace PersonDirectory.Shared.Dispatcher
{
    public interface IHandleEvent<in TEvent>
    {
        Task HandleAsync(TEvent @event, CancellationToken cancellationToken);
    }
}
