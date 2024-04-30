using PersonDirectory.Shared.DomainModels;

namespace PersonDirectory.Domain.PersonManagement.DomainEvents
{
    public class PersonCreatedDomainEvent : DomainEvent
    {
        public PersonCreatedDomainEvent(Person person) =>
            Person = person;

        public Person Person { get; private set; }
    }
}
