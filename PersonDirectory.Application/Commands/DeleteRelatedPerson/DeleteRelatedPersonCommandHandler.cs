using Microsoft.EntityFrameworkCore;
using PersonDirectory.Shared.Infrastructure;
using PersonDirectory.Domain.PersonManagement.Repository;

namespace PersonDirectory.Application.Commands.DeleteRelatedPerson
{
    public class DeleteRelatedPersonCommandHandler : BaseCommandHandler<DeleteRelatedPersonCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonRepository _personRepository;
        public DeleteRelatedPersonCommandHandler(IUnitOfWork unitOfWork,
                                                 IPersonRepository personRepository)
        {
            _unitOfWork = unitOfWork;
            _personRepository = personRepository;
        }

        public async override Task<BaseCommandResult> Handle(DeleteRelatedPersonCommand command, CancellationToken cancellationToken)
        {
            var person = await _personRepository.Query(person => person.Id == command.PersonId)
                                                .Include(person => person.RelatedPersons)
                                                .SingleOrDefaultAsync();

            if (person == null)
                return await FailAsync(ErrorCode.NotFound);

            person.RemoveRelatedPerson(command.RelatedPersonId);

            await _personRepository.UpdateAsync(person, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            return await OkAsync(person.Id);
        }
    }
}
