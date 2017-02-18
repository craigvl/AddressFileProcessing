using System;

namespace AddressFileProcessing.Models
{
    internal class NameFrequency : IEquatable<NameFrequency>
    {
        public NameFrequency(string name,  int frequency)
        {
            Name = name;
            Frequency = frequency;
        }

        public string Name { get; set; }
        public int Frequency { get; set; }

        // for unit tests mostly
        public bool Equals(NameFrequency other)
         => other != null && Name.Equals(other.Name) && Frequency.Equals(other.Frequency);

        public string ToCsvEntry(char separator = ',') => $"{Name}{separator}{Frequency}";
    }
}