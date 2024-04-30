using Microsoft.EntityFrameworkCore;
using PersonDirectory.Shared.Infrastructure;
using PersonDirectory.Domain.PersonManagement.Repository;

namespace PersonDirectory.Application.Commands.CreateRelatedPerson
{
    public class CreateRelatedPersonCommandHandler : BaseCommandHandler<CreateRelatedPersonCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonRepository _personRepository;
        public CreateRelatedPersonCommandHandler(IUnitOfWork unitOfWork,
                                                 IPersonRepository personRepository)
        {
            _unitOfWork = unitOfWork;
            _personRepository = personRepository;
        }

        public async override Task<BaseCommandResult> Handle(CreateRelatedPersonCommand command, CancellationToken cancellationToken)
        {
            var person = await _personRepository.Query(person => person.Id == command.PersonId)
                                                .Include(person => person.RelatedPersons)
                                                .SingleOrDefaultAsync();

            if (person == null)
                return await FailAsync(ErrorCode.NotFound);

            person.AddRelatedPerson(command.NewRelatedPersonId, command.RelationshipType);

            await _personRepository.UpdateAsync(person, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            return await OkAsync(person.Id);
        }
    }
}
