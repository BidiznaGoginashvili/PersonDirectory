using PersonDirectory.Shared.Infrastructure;
using PersonDirectory.Domain.PersonManagement.Repository;

namespace PersonDirectory.Application.Commands.DeletePerson
{
    public class DeletePersonCommandHandler : BaseCommandHandler<DeletePersonCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonRepository _personRepository;
        public DeletePersonCommandHandler(IUnitOfWork unitOfWork,
                                          IPersonRepository personRepository)
        {
            _unitOfWork = unitOfWork;
            _personRepository = personRepository;
        }

        public async override Task<BaseCommandResult> Handle(DeletePersonCommand command, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(command.Id, cancellationToken);

            if (person == null)
                return await FailAsync(ErrorCode.NotFound);

            person.Delete();

            await _personRepository.DeleteAsync(person, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            return await OkAsync(person.Id);
        }
    }
}
