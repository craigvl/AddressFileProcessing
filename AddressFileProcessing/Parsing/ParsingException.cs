using System;

namespace AddressFileProcessing.Parsing
{
    internal class ParsingException : Exception
    {
        protected ParsingException(string message) : base(message) { }

        public static ParsingException FromMissingFields(string input, int expectedFieldsCount, int actualFieldsCount) 
            => new ParsingException($"Not enough fields found when parsing input: '{input}'. Expected: '{expectedFieldsCount}', actual: '{actualFieldsCount}'.");

        public static ParsingException FromErroneousField(string input, int fieldIndex, string fieldValue) 
            => new ParsingException($"Error when parsing input: '{input}'. Field index: '{fieldIndex}' has erroneous value: '{fieldValue}'.");
    }
}