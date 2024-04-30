using FluentValidation;
using PersonDirectory.Application.Resources;
using PersonDirectory.Domain.CityManagement;
using PersonDirectory.Infrastructure.Database;
using PersonDirectory.Application.Commands.CreatePerson;

namespace PersonDirectory.Application.Commands.ChangePerson
{
    internal class ChangePersonCommandValidator : AbstractValidator<ChangePersonCommand>
    {
        private readonly DatabaseContext _context;

        public ChangePersonCommandValidator(DatabaseContext context)
        {
            _context = context;

            RuleFor(command => command.CityId)
               .Must(CityIdExists).WithMessage("InvalidCityId".GetLocalizedResource());

            RuleFor(command => command.FirstName)
                       .NotEmpty().WithErrorCode("FirstNameRequired".GetLocalizedResource())
                       .Length(2, 50).WithErrorCode("InvalidFirstNameLength".GetLocalizedResource())
                       .Matches(@"^([a-zA-Z]+|[\u10A0-\u10FF]+)$").WithErrorCode("InvalidFirstNameCharacters".GetLocalizedResource());

            RuleFor(command => command.LastName)
                .NotEmpty().WithErrorCode("LastNameRequired".GetLocalizedResource())
                .Length(2, 50).WithErrorCode("InvalidLastNameLength".GetLocalizedResource())
                .Matches(@"^([a-zA-Z]+|[\u10A0-\u10FF]+)$").WithErrorCode("InvalidLastNameCharacters".GetLocalizedResource());

            RuleFor(command => command.IdentificationNumber)
                .NotEmpty().WithErrorCode("IdentificationNumberRequired".GetLocalizedResource())
                .Length(11).WithErrorCode("InvalidIdentificationNumberLength".GetLocalizedResource());

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
