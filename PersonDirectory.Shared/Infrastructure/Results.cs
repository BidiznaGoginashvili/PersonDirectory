namespace PersonDirectory.Shared.Infrastructure
{
    public record BaseCommandResult(bool success);
    public record SuccessCommandResult(int id) : BaseCommandResult(true);
    public record ErrorCommandResult(ErrorCode errorCode) : BaseCommandResult(false);

    public record BaseQueryResult<TResult>(bool success);
    public record SuccessQueryResult<TResult>(TResult result) : BaseQueryResult<TResult>(true);
    public record ErrorQueryResult<TResult>(ErrorCode errorCode) : BaseQueryResult<TResult>(false);
}
