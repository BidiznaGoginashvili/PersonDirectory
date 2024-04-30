using PersonDirectory.Shared.Infrastructure;

namespace PersonDirectory.Application.Commands.DeleteRelatedPerson
{
    public class DeleteRelatedPersonCommand : BaseCommand
    {
        public int PersonId { get; set; }
        public int RelatedPersonId { get; set; }
    }
}
