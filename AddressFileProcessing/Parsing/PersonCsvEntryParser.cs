using System;
using AddressFileProcessing.Models;

namespace AddressFileProcessing.Parsing
{
    /// <summary>
    /// Parser for an input csv record (person).
    /// </summary>
    internal class PersonCsvEntryParser
    {
        private const int FieldsCount = 3; // FirstName, LastName, Address
        private readonly char _fieldSeparator;

        public PersonCsvEntryParser(char fieldSeparator = ',')
        {
            _fieldSeparator = fieldSeparator;
        }

        public Person Parse(string input)
        {
            // get required fields
            var fields = input.Split(new[] {_fieldSeparator}, StringSplitOptions.RemoveEmptyEntries);

            if (fields.Length < 3) throw CsvEntryParsingException.FromMissingFields(input, FieldsCount, fields.Length);

            // get address fields
            var addressFields = fields[2].Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);

            if(addressFields.Length < 2)  throw CsvEntryParsingException.FromErroneousField(input, 2, fields[2]);
            
            return new Person(fields[0], fields[1], addressFields[1], addressFields[0]);
        }
    }
}
