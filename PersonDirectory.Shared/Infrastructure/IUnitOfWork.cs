namespace PersonDirectory.Shared.Infrastructure
{
    public interface IUnitOfWork
    {
        Task CommitAsync(CancellationToken cancellationToken);
    }
}
