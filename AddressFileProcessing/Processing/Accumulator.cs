using System.Collections.Generic;
using System.Linq;
using AddressFileProcessing.Models;

namespace AddressFileProcessing.Processing
{
    /// <summary>
    /// This component calculate the name frequencies & List of Street for the PersonAddress items
    /// contained. It could also be seen roughly as a 'PersonAddress' repository
    /// It is a separate component from the 'Processor' to allow testing on its own
    /// It allows an append & clear, if at some point we decide to feed it data by chunk 
    /// </summary>
    internal class Accumulator
    {
        private readonly List<PersonAddress> _addresses;

        public Accumulator()
        {
            _addresses = new List<PersonAddress>();
        }

        /// <summary>
        /// Add more data to the accumulator/repository
        /// </summary>
        /// <param name="personAddresses"></param>
        public void Append(IEnumerable<PersonAddress> personAddresses)
        {
            _addresses.AddRange(personAddresses);
        }

        /// <summary>
        /// Flush
        /// </summary>
        public void Clear()
        {
            _addresses.Clear();
        }

        /// <summary>
        /// Get a list of names frequency for the records collected 
        /// </summary>
        /// <returns>List of name frequency (FirstName, LastName, Frequency) sorted by Highest Frequency, then FirstName and LastName alphabetically 
        /// </returns>
        public IReadOnlyCollection<PersonFrequency> GetNameFrequencies() =>
            _addresses
                .GroupBy(personAddress => new {personAddress.FirstName, personAddress.LastName})
                .Select(group => new PersonFrequency(group.Key.FirstName, group.Key.LastName, group.Count()))
                .OrderByDescending(person => person.Frequency)
                .ThenBy(person => person.FirstName)
                .ThenBy(person => person.LastName)
                .ToList()
                .AsReadOnly();

        /// <summary>
        /// Get a list of distinct address for the records collected
        /// </summary>
        /// <returns>List of all addresses, sorted by StreetName alphabetically </returns>
        public IReadOnlyCollection<Address> GetOrderedAddresses() =>                    
            _addresses
                .Select(personAddress => personAddress.Address)
                .OrderBy(address => address.StreetName)
                .ToList()
                .AsReadOnly();
    }
}