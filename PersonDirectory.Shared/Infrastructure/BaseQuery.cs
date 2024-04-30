using MediatR;

namespace PersonDirectory.Shared.Infrastructure
{
    public class BaseQuery<TResult> : IRequest<BaseQueryResult<TResult>>
    {
    }
}
