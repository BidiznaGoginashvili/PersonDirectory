using FluentValidation;
using PersonDirectory.Application.Resources;

namespace PersonDirectory.Application.Commands.CreateRelatedPerson
{
    public class CreateRelatedPersonCommandValidator : AbstractValidator<CreateRelatedPersonCommand>
    {
        public CreateRelatedPersonCommandValidator()
        {
            RuleFor(command => command.PersonId)
                .GreaterThan(0)
                .WithMessage("PersonIdRequired".GetLocalizedResource());

            RuleFor(command => command.NewRelatedPersonId)
                .GreaterThan(0)
                .WithMessage("RelatedPersonIdRequired".GetLocalizedResource());
        }
    }
}
