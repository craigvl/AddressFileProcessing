using System.Collections.Generic;

namespace AddressFileProcessing.IO.Reader
{
    public interface IEntriesProvider
    {
        IEnumerable<string> ReadAll();
    }
}