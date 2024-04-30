using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonDirectory.Shared.Infrastructure;

namespace PersonDirectory.Api.Infrastructure
{
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        public BaseApiController(IMediator mediator) =>
            _mediator = mediator;

        protected async Task<IActionResult> RunCommandAsync(BaseCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (result is SuccessCommandResult successResult)
                return Ok(successResult);

            if (result is ErrorCommandResult errorResult)
            {
                if (errorResult.errorCode == ErrorCode.NotFound)
                    return NotFound();

                return BadRequest(errorResult);
            }

            return BadRequest();
        }

        protected async Task<IActionResult> RunQueryAsync<TResult>(BaseQuery<TResult> query, CancellationToken cancellationToken, bool cachable = true)
        {
            var result = await _mediator.Send(query, cancellationToken);

            if (result is SuccessQueryResult<TResult> successResult)
                return Ok(successResult.result);

            if (result is ErrorQueryResult<TResult> errorResult)
            {
                if (errorResult.errorCode == ErrorCode.NotFound)
                    return NotFound();
            }

            return BadRequest();
        }
    }
}
