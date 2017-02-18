using System.Collections.Generic;

namespace AddressFileProcessing.IO.Writer
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEntriesWriter
    {
        void Save(IEnumerable<string> entries);
    }
}