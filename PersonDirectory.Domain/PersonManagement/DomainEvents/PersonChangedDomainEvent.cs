using PersonDirectory.Shared.DomainModels;

namespace PersonDirectory.Domain.PersonManagement.DomainEvents
{
    public class PersonChangedDomainEvent : DomainEvent
    {
        public PersonChangedDomainEvent(Person person) =>
            Person = person;

        public Person Person { get; private set; }
    }
}
