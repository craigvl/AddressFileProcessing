using AddressFileProcessing.Parsing;
using NUnit.Framework;

namespace AddressFileProcessing.Tests
{
    [TestFixture]
    public class PersonAddressCsvEntryParserTests
    {
        [SetUp]
        public void Init()
        {
            _parser = new PersonCsvEntryParser();
        }

        private PersonCsvEntryParser _parser;

        [Test]
        public void Parse_ValidAddress_ShouldReturnParsedAddress()
        {
            // arrange
            var expectedFirstName = "Jimmy";
            var expectedLastName = "Smith";
            var expectedStreetName = "Long Lane";
            var expectedStreetNumber = "102";

            var input = "Jimmy,Smith,102 Long Lane,29384857";

            // act 
            var actualPerson = _parser.Parse(input);

            // assert
            Assert.AreEqual(expectedFirstName, actualPerson.FirstName);
            Assert.AreEqual(expectedLastName, actualPerson.LastName);
            Assert.AreEqual(expectedStreetNumber, actualPerson.Address.StreetNumber);
            Assert.AreEqual(expectedStreetName, actualPerson.Address.StreetName);
        }

        [Test]
        public void Parse_InvalidAddressField_ShouldThrowException()
        {
            // arrange
            var erroneousAddressField = "OnlyOneField";
            var input = $"john,doe,{erroneousAddressField}";

            // act & assert
            var ex = Assert.Throws<CsvEntryParsingException>(() => _parser.Parse(input));

            Assert.That(ex.Message,
               Is.EqualTo($"Error when parsing input: '{input}'. Field index: '{2}' has erroneous value: '{erroneousAddressField}'."));

        }

        [Test]
        public void Parse_MissingFields_ShouldThrowException()
        {
            // arrange
            var input = "john,doe";

            // act & assert
            var ex = Assert.Throws<CsvEntryParsingException>(() => _parser.Parse(input));

            Assert.That(ex.Message,
                Is.EqualTo($"Not enough fields found when parsing input: '{input}'. Expected: '{3}', actual: '{2}'."));
        }
    }
}