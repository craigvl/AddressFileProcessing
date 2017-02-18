using System.Collections.Generic;

namespace AddressFileProcessing.Csv.Reader
{
    public interface IEntriesProvider
    {
        IEnumerable<string> ReadAll();
    }
}