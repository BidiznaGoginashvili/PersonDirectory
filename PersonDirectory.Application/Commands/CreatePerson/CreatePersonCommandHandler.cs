using PersonDirectory.Shared.Infrastructure;
using PersonDirectory.Domain.PersonManagement;
using PersonDirectory.Domain.PersonManagement.Repository;

namespace PersonDirectory.Application.Commands.CreatePerson
{
    public class CreatePersonCommandHandler : BaseCommandHandler<CreatePersonCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonRepository _personRepository;
        public CreatePersonCommandHandler(IUnitOfWork unitOfWork,
                                          IPersonRepository personRepository)
        {
            _unitOfWork = unitOfWork;
            _personRepository = personRepository;
        }

        public async override Task<BaseCommandResult> Handle(CreatePersonCommand command, CancellationToken cancellationToken)
        {
            var person = new Person(command.CityId,
                                    command.Gender,
                                    command.LastName,
                                    command.FirstName,
                                    command.BirthDate,
                                    command.IdentificationNumber,
                                    command.PhoneNumbers);

            await _personRepository.AddAsync(person, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            return await OkAsync(person.Id);
        }
    }
}
