using System.Linq;
using AddressFileProcessing.Models;
using NUnit.Framework;

namespace AddressFileProcessing.Tests.Accumulator
{
    [TestFixture]
    public class AccumulatorTests
    {
        [SetUp]
        public void Init()
        {
            _accumulator = new Processing.Accumulator();
        }

        private Processing.Accumulator _accumulator;

        [Test]
        public void Append_SingleEntry_ShouldResultOnlySingleInNamesAndAddresses()
        {
            // arrange
            var inputSet = new []
            {
                new PersonAddress("jean", "valjean", "champ-elysees", "40")
            };

            _accumulator.Append(inputSet);
            // act
            var actualAddresses = _accumulator.GetOrderedAddresses();
            var actualNames = _accumulator.GetNameFrequencies();

            // assert
            Assert.AreEqual(1, actualAddresses.Count);
            Assert.AreEqual(1, actualNames.Count);
        }


        [Test]
        public void Append_TwoSameNameEntry_ShouldResultAggregateSingleNameFrequency()
        {
            // arrange
            var inputSet = new []
            {
                new PersonAddress("jean", "valjean", "champ-elysees", "40"),
                new PersonAddress("jean", "valjean", "champ-elysees", "1")
            };

            // act
            _accumulator.Append(inputSet);

            var actualAddresses = _accumulator.GetOrderedAddresses();
            var actualNames = _accumulator.GetNameFrequencies();

            // assert
            Assert.AreEqual(2, actualAddresses.Count);
            Assert.AreEqual(1, actualNames.Count);
            Assert.AreEqual(2, actualNames.First().Frequency);
        }


        [Test]
        public void Append_WithSameNameEntry_ShouldResultAggregateSingleNameFrequency()
        {
            // arrange
            var inputSet = new[]
            {
                new PersonAddress("jean", "valjean", "bee lane", "1"),
                new PersonAddress("jean", "valjean", "apple st", "40"),
                new PersonAddress("jean", "valjean", "cat way", "4")
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

        [Test]
        public void Append_WithManyEntry_ShouldOrderNamesByFrequencyAndThenByFirstName()
        {
            // arrange
            var inputSet = new[]
            {
                new PersonAddress("jean", "valjean", "bee lane", "1"),
                new PersonAddress("jean", "valjean", "apple st", "40"),
                new PersonAddress("jean", "valjean", "cat way", "4"),
                new PersonAddress("charles", "dupont", "cat way", "4"),
                new PersonAddress("charles", "dupont", "apple st", "40"),
                new PersonAddress("charles", "dupont", "cat way", "4"),
                new PersonAddress("boris", "gates", "cat way", "4"),
                new PersonAddress("boris", "gates", "cat way", "4")
            };

            var expectedNames = new[]
            {
                new PersonFrequency("charles", "dupont", 3),
                new PersonFrequency("jean", "valjean", 3),
                new PersonFrequency("boris", "gates", 2)
            };

            // act           
            _accumulator.Append(inputSet);
            var actualNames = _accumulator.GetNameFrequencies();

            // assert
            Assert.AreEqual(expectedNames, actualNames);
        }
    }
}