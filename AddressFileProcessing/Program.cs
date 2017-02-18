using System;
using AddressFileProcessing.Csv.Reader;
using AddressFileProcessing.Csv.Writer;
using AddressFileProcessing.Processing;

namespace AddressFileProcessing
{
    internal class Program
    {


        private static void Main(string[] args)
        {
            Func<int, string, string> argOrDefaut =
                (argIndex, defautValue) =>
                    args.Length > argIndex && !string.IsNullOrWhiteSpace(args[argIndex])
                        ? args[argIndex]
                        : defautValue;

            var inputFile = argOrDefaut(0, "data.csv");
            var outputNamesFile = argOrDefaut(1, "names.csv");
            var outputAddressesFile = argOrDefaut(2, "addresses.csv");

            Console.WriteLine($"This file will be read: {inputFile} ");
            Console.WriteLine(
                $"Theses files for names and addresses will be created: {outputNamesFile} and {outputAddressesFile}.");

            Console.WriteLine("Processing ... ");

            try
            {
                var processor = new Processor(
                    new FileEntriesProvider(inputFile, true),
                    new FileEntriesWriter(outputNamesFile),
                    new FileEntriesWriter(outputAddressesFile)
                );

                processor.Process();
                Console.WriteLine("Processing done.");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Oups, something went wrong with the processing, details follows ... ");
                Console.WriteLine(ex.Message);


            }


        }
    }
}
