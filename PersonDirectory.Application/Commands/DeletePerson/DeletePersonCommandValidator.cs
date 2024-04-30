using FluentValidation;
using PersonDirectory.Application.Resources;

namespace PersonDirectory.Application.Commands.DeletePerson
{
    public class DeletePersonCommandValidator : AbstractValidator<DeletePersonCommand>
    {
        public DeletePersonCommandValidator()
        {
            RuleFor(command => command.Id)
                .GreaterThan(0)
                .WithMessage("PersonIdRequired".GetLocalizedResource());
        }
    }
}
