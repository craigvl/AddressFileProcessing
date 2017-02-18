using System;

namespace AddressFileProcessing.Models
{
    public class Address : IEquatable<Address>
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

        public string ToCsvEntry() => $"{StreetNumber} {StreetName}"; // only one field
    }
}



