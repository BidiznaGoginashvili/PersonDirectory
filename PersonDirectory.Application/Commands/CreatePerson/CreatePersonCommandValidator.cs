using FluentValidation;
using PersonDirectory.Application.Resources;
using PersonDirectory.Domain.CityManagement;
using PersonDirectory.Infrastructure.Database;

namespace PersonDirectory.Application.Commands.CreatePerson
{
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        private readonly DatabaseContext _context;

        public CreatePersonCommandValidator(DatabaseContext context)
        {
            _context = context;

            RuleFor(command => command.CityId)
                .Must(CityIdExists).WithMessage("InvalidCityId".GetLocalizedResource());

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

        private bool CityIdExists(int cityId) =>
                _context.Set<City>().Any(c => c.Id == cityId);

        private bool BeAtLeast18YearsOld(DateTime birthDate) =>
            DateTime.Today.AddYears(-18) >= birthDate.Date;

    }
}
