using System;

namespace AddressFileProcessing.Models
{
    public class PersonFrequency : Person, IEquatable<PersonFrequency>
    {
        public PersonFrequency(string firstName, string lastName, int frequency) : base(firstName, lastName)
        {
            Frequency = frequency;
        }

        public int Frequency { get; set; }

        // for unit tests mostly
        public bool Equals(PersonFrequency other)
         => other != null && base.Equals(other) && Frequency.Equals(other.Frequency);

        public string ToCsvEntry(char separator = ',') => $"{FirstName}{separator}{LastName}{separator}{Frequency}";
    }
}