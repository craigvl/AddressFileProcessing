using System;

namespace AddressFileProcessing.Models
{
    internal class Address : IEquatable<Address>
    {
        public Address(string streetName, string streetNumber)
        {
            StreetName = streetName;
            StreetNumber = streetNumber;
        }

        public string StreetName { get; set; }
        public string StreetNumber { get; set; }

        // mainly here for unit tests
        public bool Equals(Address other)
            => (other != null) && StreetName.Equals(other.StreetName) && StreetNumber.Equals(other.StreetNumber);

        // required for Linq Distinct() ... 
        public override int GetHashCode()
        {
            unchecked
            {
                return (StreetNumber ?? string.Empty).GetHashCode() + (StreetName ?? string.Empty).GetHashCode();
            }
        }


        public string ToCsvEntry() => $"{StreetNumber} {StreetName}"; // only one field
    }
}



