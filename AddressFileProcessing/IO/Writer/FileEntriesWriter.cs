using System.Collections.Generic;
using System.IO;

namespace AddressFileProcessing.IO.Writer
{
    /// <summary>
    /// Provide support for saving a list of entries (text lines) into a file.
    /// </summary>
    public class FileEntriesWriter : IEntriesWriter
    {
        private readonly string _filePath;

        public FileEntriesWriter(string filePath)
        {
            _filePath = filePath;
        }

        public void Save(IEnumerable<string> entries)
        {
            File.WriteAllLines(_filePath, entries);
        }
    }
}
