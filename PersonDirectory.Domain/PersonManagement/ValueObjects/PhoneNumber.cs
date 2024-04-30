using PersonDirectory.Shared.DomainModels;
using PersonDirectory.Domain.PersonManagement.Enums;

namespace PersonDirectory.Domain.PersonManagement.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public PhoneNumber(string number,
                           PhoneNumberType phoneNumberType)
        {
            Number = number;
            PhoneNumberType = phoneNumberType;
        }

        public string Number { get; private set; }
        public PhoneNumberType PhoneNumberType { get; private set; }
    }
}
