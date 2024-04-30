using FluentValidation;
using PersonDirectory.Application.Resources;

namespace PersonDirectory.Application.Commands.CreatePerson
{
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleFor(command => command.FirstName)
                .NotEmpty().WithMessage("FirstNameRequired".GetLocalizedResource())
                .Length(2, 50).WithMessage("InvalidFirstNameLength".GetLocalizedResource())
                .Matches(@"^([a-zA-Z]+|[\u10A0-\u10FF]+)$").WithMessage("InvalidFirstNameCharacters".GetLocalizedResource());

            RuleFor(command => command.LastName)
                .NotEmpty().WithMessage("LastNameRequired".GetLocalizedResource())
                .Length(2, 50).WithMessage("InvalidLastNameLength".GetLocalizedResource())
                .Matches(@"^([a-zA-Z]+|[\u10A0-\u10FF]+)$").WithMessage("InvalidLastNameCharacters".GetLocalizedResource());

            RuleFor(command => command.IdentificationNumber)
                .NotEmpty().WithMessage("IdentificationNumberRequired".GetLocalizedResource())
                .Length(11).WithMessage("InvalidIdentificationNumberLength".GetLocalizedResource());

            RuleFor(command => command.BirthDate)
                .NotEmpty().WithMessage("BirthDateRequired".GetLocalizedResource())
                .Must(BeAtLeast18YearsOld).WithMessage("InvalidBirthDate".GetLocalizedResource());
        }

        private bool BeAtLeast18YearsOld(DateTime birthDate)
        {
            return DateTime.Today.AddYears(-18) >= birthDate.Date;
        }
    }
}
