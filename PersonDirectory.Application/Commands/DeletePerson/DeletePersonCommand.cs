using PersonDirectory.Shared.Infrastructure;

namespace PersonDirectory.Application.Commands.DeletePerson
{
    public class DeletePersonCommand : BaseCommand
    {
        public int Id { get; set; }
    }
}
