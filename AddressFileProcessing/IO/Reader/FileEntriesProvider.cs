using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AddressFileProcessing.IO.Reader
{
    /// <summary>
    /// Provide support for reading a list of entries (text lines) from a file.
    /// </summary>
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
