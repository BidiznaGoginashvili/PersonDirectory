using MediatR;

namespace PersonDirectory.Shared.Infrastructure
{
    public interface IQueryHandler<TRequest, TResult> : IRequestHandler<TRequest, BaseQueryResult<TResult>> where
                                                        TRequest : IRequest<BaseQueryResult<TResult>>
    {
    }
}
