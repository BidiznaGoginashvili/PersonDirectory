namespace PersonDirectory.Shared.Infrastructure
{
    public abstract class BaseQueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : BaseQuery<TResult>
    {
        protected Task<BaseQueryResult<TResult>> OkAsync(TResult response) =>
            Task.FromResult<BaseQueryResult<TResult>>(new SuccessQueryResult<TResult>(response));

        protected Task<BaseQueryResult<TResult>> FailAsync(ErrorCode errorCode) =>
            Task.FromResult<BaseQueryResult<TResult>>(new ErrorQueryResult<TResult>(errorCode));

        public abstract Task<BaseQueryResult<TResult>> Handle(TQuery query, CancellationToken cancellationToken);
    }
}
