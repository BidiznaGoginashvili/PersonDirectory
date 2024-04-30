using MediatR;
using Newtonsoft.Json;
using FluentValidation;

namespace PersonDirectory.Shared.Infrastructure.Behaviors
{
    public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationTasks = _validators.Select(validator => validator.ValidateAsync(context, cancellationToken));
                var validationResult = await Task.WhenAll(validationTasks);

                var failures = validationResult.SelectMany(result => result.Errors)
                                               .Where(failure => failure != null)
                                               .ToList();

                if (failures.Count > 0)
                {
                    var errorMessages = failures.Select(failure => new Dictionary<string, string> { { failure.PropertyName, failure.ErrorMessage } })
                                                .ToList();

                    var serializedErrors = JsonConvert.SerializeObject(errorMessages);

                    throw new ValidationException(serializedErrors);
                }
            }

            return await next();
        }
    }
}
