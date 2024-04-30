using PersonDirectory.Shared.DomainModels;

namespace PersonDirectory.Domain.PersonManagement.DomainEvents
{
    public class PersonDeletedDomainEvent : DomainEvent
    {
        public PersonDeletedDomainEvent(int id) =>
            Id = id;

        public int Id { get; private set; }
    }
}
