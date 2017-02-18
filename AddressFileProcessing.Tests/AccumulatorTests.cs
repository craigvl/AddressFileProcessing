using System.Linq;
using AddressFileProcessing.Models;
using AddressFileProcessing.Storage;
using NUnit.Framework;

namespace AddressFileProcessing.Tests
{
    [TestFixture]
    public class AccumulatorTests
    {
        [SetUp]
        public void Init()
        {
            _accumulator = new Accumulator();
        }

        private Accumulator _accumulator;


        [Test]
        public void Append_SameNameEntry_ShouldAggregateFrequency()
        {
            // arrange
            var inputSet = new []
            {
                new Person("jo", "valjean", "champ-elysees", "40"),
                new Person("jo", "valjean", "champ-elysees", "1")
            };

            // act
            _accumulator.Append(inputSet);

            var actualAddresses = _accumulator.GetOrderedAddresses();
            var actualNames = _accumulator.GetNameFrequencies();

            // assert
            Assert.AreEqual(2, actualAddresses.Count);
            Assert.AreEqual(2, actualNames.Count);
            Assert.AreEqual(2, actualNames.First().Frequency);
            Assert.AreEqual(2, actualNames.Last().Frequency);
        }

        [Test]
        public void Append_SingleEntry_ShouldReturnTwoNamesAndOneAddresses()
        {
            // arrange
            var inputSet = new []
            {
                new Person("jo", "valjean", "champ-elysees", "40")
            };

            _accumulator.Append(inputSet);
            // act
            var actualAddresses = _accumulator.GetOrderedAddresses();
            var actualNames = _accumulator.GetNameFrequencies();

            // assert
            Assert.AreEqual(1, actualAddresses.Count);
            Assert.AreEqual(2, actualNames.Count);
        }

        [Test]
        public void Append_SingleEntryWithSameName_ShouldAggregateIntoOneName()
        {
            // arrange
            var inputSet = new[]
            {
                new Person("jo", "valjean", "champ-elysees", "40")
            };

            _accumulator.Append(inputSet);
            // act
            var actualAddresses = _accumulator.GetOrderedAddresses();
            var actualNames = _accumulator.GetNameFrequencies();

            // assert
            Assert.AreEqual(1, actualAddresses.Count);
            Assert.AreEqual(1, actualNames.Count);
            Assert.AreEqual(2, actualNames.Single().Frequency);

        }

        [Test]
        public void Append_WithManyEntry_ShouldOrderNamesByFrequencyAndThenByFirstName()
        {
            // arrange
            var inputSet = new[]
            {
                new Person("jean",    "valjean", "bee lane", "1"),
                new Person("charles", "valjean", "apple st", "40"),
                new Person("valjean", "dupont",  "cat way",  "1"),
                new Person("boris",   "charles", "cat way",  "2"),
                new Person("boris",   "gates",   "cat way",  "3")
            };

            var expectedNames = new[]
            {
                new NameFrequency("valjean", 3),
                new NameFrequency("boris", 2),
                new NameFrequency("charles", 2),
                new NameFrequency("dupont", 1),
                new NameFrequency("gates", 1),
                new NameFrequency("jean", 1)
            };

            // act           
            _accumulator.Append(inputSet);
            var actualNames = _accumulator.GetNameFrequencies();

            // assert
            Assert.AreEqual(expectedNames, actualNames);
        }


        [Test]
        public void Append_WithSameNameEntry_ShouldAggregateSingleNameFrequency()
        {
            // arrange
            var inputSet = new[]
            {
                new Person("jean", "jean", "bee lane", "1"),
                new Person("jean", "jean", "apple st", "40"),
                new Person("jean", "jean", "cat way", "4")
            };

            var expectedAddresses = new[]
            {
                new Address("apple st", "40"),
                new Address("bee lane", "1"),
                new Address("cat way", "4")
            };

            // act           
            _accumulator.Append(inputSet);
            var actualAddresses = _accumulator.GetOrderedAddresses();

            // assert
            CollectionAssert.AreEqual(expectedAddresses, actualAddresses);
        }
    }
}