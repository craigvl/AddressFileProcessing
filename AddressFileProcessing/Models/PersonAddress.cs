namespace AddressFileProcessing.Models
{
    public class PersonAddress : Person
    {
        public PersonAddress(string firstName, string lastName, string streetName, string streetNumber) : base(firstName, lastName)
        {
            Address = new Address(streetName, streetNumber);
        }

        public Address Address { get; set; }
    }
}
