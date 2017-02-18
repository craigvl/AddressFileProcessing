using System.Collections.Generic;

namespace AddressFileProcessing.Csv.Writer
{
    public interface IEntriesWriter
    {
        void Save(IEnumerable<string> entries);
    }
}