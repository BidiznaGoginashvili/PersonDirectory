using PersonDirectory.Shared.Infrastructure;
using PersonDirectory.Domain.PersonManagement.Repository;

namespace PersonDirectory.Application.Commands.ChangePerson
{
    public class ChangePersonCommandHandler : BaseCommandHandler<ChangePersonCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonRepository _personRepository;
        public ChangePersonCommandHandler(IUnitOfWork unitOfWork,
                                          IPersonRepository personRepository)
        {
            _unitOfWork = unitOfWork;
            _personRepository = personRepository;
        }

        public async override Task<BaseCommandResult> Handle(ChangePersonCommand command, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(command.Id, cancellationToken);

            if (person == null)
                return await FailAsync(ErrorCode.NotFound);

            person.ChangeDetails(command.CityId,
                                 command.Gender,
                                 command.LastName,
                                 command.FirstName,
                                 command.PhotoPath,
                                 command.BirthDate,
                                 command.IdentificationNumber,
                                 command.PhoneNumbers);

            await _personRepository.UpdateAsync(person, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            return await OkAsync(person.Id);
        }
    }
}