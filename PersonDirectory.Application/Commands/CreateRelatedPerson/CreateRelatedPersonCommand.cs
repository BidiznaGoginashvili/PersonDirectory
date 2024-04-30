using PersonDirectory.Shared.Infrastructure;
using PersonDirectory.Domain.PersonManagement.Enums;

namespace PersonDirectory.Application.Commands.CreateRelatedPerson
{
    public class CreateRelatedPersonCommand : BaseCommand
    {
        public int PersonId { get; set; }
        public int NewRelatedPersonId { get; set; }
        public RelationshipType RelationshipType { get; set; }
    }
}
