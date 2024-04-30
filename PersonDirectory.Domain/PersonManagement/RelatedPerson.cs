using PersonDirectory.Shared.DomainModels;
using PersonDirectory.Domain.PersonManagement.Enums;

namespace PersonDirectory.Domain.PersonManagement
{
    public class RelatedPerson : Entity
    {
        public RelatedPerson() { }

        public RelatedPerson(int personId,
                             int relatedPersonId,
                             RelationshipType relationshipType)
        {
            PersonId = personId;
            RelatedPersonId = relatedPersonId;
            RelationshipType = relationshipType;
        }

        public int PersonId { get; private set; }
        public int RelatedPersonId { get; private set; }
        public RelationshipType RelationshipType { get; private set; }
    }
}
