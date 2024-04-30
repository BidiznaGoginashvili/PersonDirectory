using MediatR;

namespace PersonDirectory.Shared.Infrastructure
{
    public class BaseCommand : IRequest<BaseCommandResult>
    {
    }
}
