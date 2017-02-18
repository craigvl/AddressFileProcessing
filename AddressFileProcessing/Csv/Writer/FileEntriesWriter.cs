using System.Collections.Generic;
using System.IO;

namespace AddressFileProcessing.Csv.Writer
{
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
