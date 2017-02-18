using System;

namespace AddressFileProcessing.Models
{
    public class Person : IEquatable<Person>
    {
        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        // mainly here for unit tests
        public bool Equals(Person other)
            => (other != null) && FirstName.Equals(other.FirstName) && LastName.Equals(other.LastName);
    }
}