using FluentValidation;
using PersonDirectory.Application.Resources;

namespace PersonDirectory.Application.Commands.DeleteRelatedPerson
{
    public class DeleteRelatedPersonCommandValidator : AbstractValidator<DeleteRelatedPersonCommand>
    {
        public DeleteRelatedPersonCommandValidator()
        {
            RuleFor(command => command.PersonId)
               .GreaterThan(0)
               .WithMessage("PersonIdRequired".GetLocalizedResource());

            RuleFor(command => command.RelatedPersonId)
                .GreaterThan(0)
                .WithMessage("RelatedPersonIdRequired".GetLocalizedResource());
        }
    }
}
