using System.Linq;
using AddressFileProcessing.IO.Reader;
using AddressFileProcessing.IO.Writer;
using AddressFileProcessing.Parsing;
using AddressFileProcessing.Storage;

namespace AddressFileProcessing.Processing
{
    /// <summary>
    /// Self contained component to orchestrate a run. 
    /// 1) Read from the provider, 
    /// 2) Accumulate/store the data,
    /// 3) Generate the results
    /// 4) Persist them.
    /// </summary>
    internal class Processor
    {
        private readonly IEntriesWriter _nameFrequenciesWriter;
        private readonly IEntriesWriter _orderedAddressesWriter;
        private readonly IEntriesProvider _personAddressProvider;

        public Processor(
            IEntriesProvider personAddressProvider,
            IEntriesWriter nameFrequenciesWriter,
            IEntriesWriter orderedAddressesWriter
            )
        {
            _personAddressProvider = personAddressProvider;
            _nameFrequenciesWriter = nameFrequenciesWriter;
            _orderedAddressesWriter = orderedAddressesWriter;
        }

        public void Process()
        {
            var personAddressEntryParser = new PersonCsvEntryParser();
            var accumulator = new Accumulator();

            // collect and parse the entries from the data provider
            var entries = _personAddressProvider
                .ReadAll()
                .Select(personAddressEntryParser.Parse);

            // store the entries in the accumulator
            accumulator.Append(entries);

            // get the result, persist them to the writers
            _nameFrequenciesWriter.Save(accumulator.GetNameFrequencies().Select(name => name.ToCsvEntry()));
            _orderedAddressesWriter.Save(accumulator.GetOrderedAddresses().Select(address => address.ToCsvEntry()));
        }
    }
}
