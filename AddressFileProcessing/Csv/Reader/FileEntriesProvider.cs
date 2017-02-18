using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AddressFileProcessing.Csv.Reader
{
    internal class FileEntriesProvider : IEntriesProvider
    {
        private readonly bool _excludeHeader;
        private readonly string _filePath;

        public FileEntriesProvider(string fileName, bool excludeHeader)
        {
            _filePath = fileName;
            _excludeHeader = excludeHeader;
        }

        public IEnumerable<string> ReadAll() =>
            File.ReadAllLines(_filePath)
                .Skip(_excludeHeader ? 1 : 0);
    }
}
