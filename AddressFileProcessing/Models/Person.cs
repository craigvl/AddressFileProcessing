namespace AddressFileProcessing.Models
{
    internal class Person 
    {
        public Person(string firstName, string lastName, string streetName, string streetNumber) 
        {
            Address = new Address(streetName, streetNumber);
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Address Address { get; set; }

        // mainly here for unit tests
        public bool Equals(Person other)
            => (other != null) && FirstName.Equals(other.FirstName) && LastName.Equals(other.LastName) && Address.Equals(other.Address);
    }
}
