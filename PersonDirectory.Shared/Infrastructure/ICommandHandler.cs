using MediatR;

namespace PersonDirectory.Shared.Infrastructure
{
    public interface ICommandHandler<TRequest> : IRequestHandler<TRequest, BaseCommandResult> where TRequest
                                               : BaseCommand
    {
    }
}
