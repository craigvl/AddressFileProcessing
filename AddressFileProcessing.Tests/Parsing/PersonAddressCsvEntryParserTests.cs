using AddressFileProcessing.Parsing;
using NUnit.Framework;

namespace AddressFileProcessing.Tests.Parsing
{
    [TestFixture]
    public class PersonAddressCsvEntryParserTests
    {
        [SetUp]
        public void Init()
        {
            _parser = new PersonAddressCsvEntryParser();
        }

        private PersonAddressCsvEntryParser _parser;

        [Test]
        public void Parse_WithInvalidAddressField_ShouldThrowException()
        {
            // arrange
            var input = "OnlyOneField";

            // act & assert
            Assert.Throws<ParsingException>(() => _parser.Parse(input),
                 $"Error when parsing input: '{input}'. Field index: '{2}' has erroneous value: '{input}'.");
        }


        [Test]
        public void Parse_WithMissingFields_ShouldThrowException()
        {
            // arrange
            var input = "OnlyOneField";

            // act & assert
            Assert.Throws<ParsingException>(() => _parser.Parse(input),
                $"Not enough fields found when parsing input: '{input}'. Expected: '{3}', actual: '{1}'.");
        }


        [Test]
        public void Parse_WithValidAddress_ShouldReturnParsedAddress()
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
    }
}