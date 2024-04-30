namespace PersonDirectory.Shared.Infrastructure
{
    public abstract class BaseCommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : BaseCommand
    {
        public Task<BaseCommandResult> OkAsync(int id) =>
            Task.FromResult<BaseCommandResult>(new SuccessCommandResult(id));

        public Task<BaseCommandResult> FailAsync(ErrorCode errorCode) =>
            Task.FromResult<BaseCommandResult>(new ErrorCommandResult(errorCode));

        public abstract Task<BaseCommandResult> Handle(TCommand command, CancellationToken cancellationToken);
    }
}
