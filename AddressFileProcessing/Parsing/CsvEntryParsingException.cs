using System;

namespace AddressFileProcessing.Parsing
{
    /// <summary>
    /// Parsing exception, give a bit more explanation on what has gone wrong when parsing a csv row
    /// </summary>
    internal class CsvEntryParsingException : Exception
    {
        protected CsvEntryParsingException(string message) : base(message) { }

        public static CsvEntryParsingException FromMissingFields(string input, int expectedFieldsCount, int actualFieldsCount) 
            => new CsvEntryParsingException($"Not enough fields found when parsing input: '{input}'. Expected: '{expectedFieldsCount}', actual: '{actualFieldsCount}'.");

        public static CsvEntryParsingException FromErroneousField(string input, int fieldIndex, string fieldValue) 
            => new CsvEntryParsingException($"Error when parsing input: '{input}'. Field index: '{fieldIndex}' has erroneous value: '{fieldValue}'.");
    }
}