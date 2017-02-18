using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AddressFileProcessing.Csv.Reader;
using AddressFileProcessing.Csv.Writer;
using AddressFileProcessing.Processing;
using Moq;
using NUnit.Framework;

namespace AddressFileProcessing.Tests
{
    [TestFixture]
    public class ProcessorTests
    {
        [SetUp]
        public void Init()
        {
            // we mock the provider so that we can feed
            _personAddressReader = new Mock<IEntriesProvider>();
            _namesFrequencyWriter = new Mock<IEntriesWriter>();
            _addressesWriter = new Mock<IEntriesWriter>();

            _processor = new Processor(_personAddressReader.Object, _addressesWriter.Object, _namesFrequencyWriter.Object);
        }

        private Mock<IEntriesProvider> _personAddressReader;
        private Mock<IEntriesWriter> _namesFrequencyWriter;
        private Mock<IEntriesWriter> _addressesWriter;
        private Processor _processor;

        [Test]
        public void Process_WithGivenData_ShouldReturnCorrectNamesAndAddressesSets()
        {
            // arrange
            var inputEntries = new[]
            {
                "Jimmy,Smith,102 Long Lane,29384857",
                "Clive,Owen,65 Ambling Way,31214788",
                "James,Brown,82 Stewart St,32114566",
                "Graham,Howe,12 Howard St,8766556",
                "John,Howe,78 Short Lane,29384857",
                "Clive,Smith,49 Sutherland St,31214788",
                "James,Owen,8 Crimson Rd,32114566",
                "Graham,Brown,94 Roland St,8766556"
            };

            var expectedNameFrequencies = new[]
            {
                "Brown,2",
                "Clive,2",
                "Graham,2",
                "Howe,2",
                "James,2",
                "Owen,2",
                "Smith,2",
                "Jimmy,1",
                "John,1"
            };

            var expectedAddresses = new[]
            {
                "65 Ambling Way",
                "8 Crimson Rd",
                "12 Howard St",
                "102 Long Lane",
                "94 Roland St",
                "78 Short Lane",
                "82 Stewart St",
                "49 Sutherland St"
            };

            // substitute the read from the provider to provide the sample entries
            _personAddressReader
                .Setup(reader => reader.ReadAll())
                .Returns(inputEntries);

           
            // act
            _processor.Process();

            // assert

            // expressions to check that the dataset are correct (this notation is required by the moq framework ... )
            Expression<Func<IEnumerable<string>, bool>> matchNameFrequencies = names => expectedNameFrequencies.SequenceEqual(names);
            Expression<Func<IEnumerable<string>, bool>> matchAddresses = addresses => expectedAddresses.SequenceEqual(addresses);

            //add a spy on the 'save' method of the writers which allow us to check if what if the data set saved are correct
            _addressesWriter.Verify(writer => writer.Save(It.Is(matchAddresses)), Times.Once);
            _namesFrequencyWriter.Verify(writer => writer.Save(It.Is(matchNameFrequencies)), Times.Once);
        }
    }
}