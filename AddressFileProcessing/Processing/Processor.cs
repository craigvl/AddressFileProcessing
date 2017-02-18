using System.Linq;
using AddressFileProcessing.Csv.Reader;
using AddressFileProcessing.Csv.Writer;
using AddressFileProcessing.Parsing;
using AddressFileProcessing.Storage;

namespace AddressFileProcessing.Processing
{
    internal class Processor
    {
        private readonly IEntriesWriter _nameFrequenciesWriter;
        private readonly IEntriesWriter _orderedAddressesWriter;
        private readonly IEntriesProvider _personAddressProvider;

        public Processor(
            IEntriesProvider personAddressProvider, 
            IEntriesWriter orderedAddressesWriter,
            IEntriesWriter nameFrequenciesWriter
            )
        {
            _personAddressProvider = personAddressProvider;
            _orderedAddressesWriter = orderedAddressesWriter;
            _nameFrequenciesWriter = nameFrequenciesWriter;
        }

        public void Process()
        {
            var personAddressEntryParser = new PersonCsvEntryParser();
            var accumulator = new Accumulator();

            // collect entries from the text lines provider
            var entries = _personAddressProvider
                .ReadAll()
                .Select(personAddressEntryParser.Parse);

            accumulator.Append(entries);

            _orderedAddressesWriter.Save(accumulator.GetOrderedAddresses().Select(address => address.ToCsvEntry()));
            _nameFrequenciesWriter.Save(accumulator.GetNameFrequencies().Select(name => name.ToCsvEntry()));
        }
    }
}
