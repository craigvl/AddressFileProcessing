using System.Collections.Generic;
using System.Linq;
using AddressFileProcessing.Models;

namespace AddressFileProcessing.Storage
{
    /// <summary>
    /// This component calculate the name frequencies & List of Street for the Person items
    /// contained. It could also be seen roughly as a 'Person' repository
    /// It is a separate component from the 'Processor' to allow testing on its own
    /// It allows an append & clear, if at some point we decide to feed it data by chunk 
    /// </summary>
    internal class Accumulator
    {
        private readonly List<Person> _persons;

        public Accumulator()
        {
            _persons = new List<Person>();
        }

        /// <summary>
        /// Add more data to the accumulator/repository
        /// </summary>
        /// <param name="personAddresses"></param>
        public void Append(IEnumerable<Person> personAddresses)
        {
            _persons.AddRange(personAddresses);
        }

        /// <summary>
        /// Flush
        /// </summary>
        public void Clear()
        {
            _persons.Clear();
        }

        /// <summary>
        /// Get a list of names frequency for the records collected by merging first and Last Name in the same list
        /// </summary>
        /// <returns>List of name frequency (Name, Frequency) sorted by Highest Frequency, then Name alphabetically 
        /// </returns>
        public IReadOnlyCollection<NameFrequency> GetNameFrequencies() =>
            _persons
                .Select(person => person.FirstName)
                .Concat(_persons.Select(person => person.LastName))
                .GroupBy(name => name)
                .Select(group => new NameFrequency(group.Key, group.Count()))
                .OrderByDescending(person => person.Frequency)
                .ThenBy(nameFrequency => nameFrequency.Name)
                .ToList()
                .AsReadOnly();

        /// <summary>
        /// Get a list of distinct address for the records collected
        /// </summary>
        /// <returns>List of all addresses, sorted by StreetName alphabetically </returns>
        public IReadOnlyCollection<Address> GetOrderedAddresses() =>                    
            _persons
                .Select(personAddress => personAddress.Address)
                .Distinct()
                .OrderBy(address => address.StreetName)
                .ToList()
                .AsReadOnly();
    }
}