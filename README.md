# Summary (where do I start?)

## Content

* Proposed solution source code with C# with Visual Studio 2015.
* The executable as well as the input and output files can be found at the root of this repo, within `AddressFileProcessing.zip`  	

## Prerequisites

* MS .net Framework 4.6.1
* Git to get the code (optional)
* Visual Studio 2013 or 2015 to open and edit it (optional)

## Run the executable (command line)

The executable (AddressFileProcessing.exe) should be used with 3 parameters (if not provided they will be set to : _data.csv_ _names.csv_ _addresses.csv_ 

```
AddressFileProcessing <inputfile> <namefile> <addressesfile>
```

The input file must be located in the same folder as the executable. The output files will be created in this folder too, given that program executed successfully.

## Run in Debug Mode (Visual Studio 2015)

* Open the .sln file
* Run the main project (AddressFileProcessing) in debug mode (the sample input file included, data.csv will be automatically copied to the /bin folder)

## Output

Command line
```
> AddressFileProcessing.exe data.csv names.csv addresses.csv

This file will be read: data.csv
Processing ...
Processing done.
The output files for names and addresses have been created: names.csv and addresses.csv.
Press 'Enter' to exit.
```

data.csv
```
FirstName,LastName,Address,PhoneNumber
Jimmy,Smith,102 Long Lane,29384857
Clive,Owen,65 Ambling Way,31214788
James,Brown,82 Stewart St,32114566
Graham,Howe,12 Howard St,8766556
John,Howe,78 Short Lane,29384857
Clive,Smith,49 Sutherland St,31214788
James,Owen,8 Crimson Rd,32114566
Graham,Brown,94 Roland St,8766556
```

addresses.csv
```
65 Ambling Way
8 Crimson Rd
12 Howard St
102 Long Lane
94 Roland St
78 Short Lane
82 Stewart St
49 Sutherland St
```

names.csv
```
Brown,2
Clive,2
Graham,2
Howe,2
James,2
Owen,2
Smith,2
Jimmy,1
John,1
```

## Project Structure (where is what?)
```
/IO
   /Reader : component to read a text file and a generic interface for reading text content
   /Writer : component to write a text file and a generic interface for writing text content

/Models : models representing what is read from the input (_persons_) and what is persisted to the output (_names_, _addresses_) 

/Parsing : helper to parse the input rows
  
/Storage : contains the accumulator ( = repository to store the content read and get the aggregated results _names_, _addresses_ from it)

/Processing : contains the processor that orchestrate and perform a run on the input file and generate the output

Program.cs : entry point to run the processing
```

# Design (why it is done this way?)

## Choices / decision / assumptions

In order to ease the testing I tried to decompose the app in small independent components (by responsibility) as much as I could. 
That way I could create a component, test it, then move to the next one, ensuring that they all work properly.

The _phonenumber_ field has been voluntarily omitted as we don't use it in the application.

The _addresses_ ouput will not contains duplicated addresses: if many persons live at a same address, it will only output one row in the _addresses_ file.

If the parsing of one line from the input fails, the whole file is rejected and the processing will not execute, this would forces the user to fix the file. This is an arbitrary decision, another way would be to ignore the malformed lines and notify the user.

The file `readers` and `writers` both implement an interface, which at a first glance might seem not necessary, but it
helps for the unit tests; instead of having to provide a concrete reader/writer specific for text files to the `processor`, a mocked reader/writer 
with in-memory dataset could be created and passed to the `processor`. That way we can inject in-memory data from the `reader` and 
check that the content is generated properly on write from the `writer`. Also if we wanted to extend this application to use others 
types of input/output (such as DB, Console input/output, Json files ... ) we could by simply creating new reader/writer (without changing anything else) and pass it to the processor.

# Testing (does it actually work?)

## Summary

`NUnit` is used as a testing framework. 
`Moq` as substitution/mocking/faking tool.

The critical components have been unit tested:

* **Parser:** to check that it fails when required and pass when result is valid.
* **Accumulator:** to verify that the query to calculate the _names frequencies_ & _address_ follows the instruction.
* **Processor:** just to verify that the example given in the instructions works as defined in them.

The unit tests can be run in VS 2015.

# Possible improvements

* More components/methods could have been tested and more tests cases could have been added, especially to test all the edge cases. For the purpose of this exercise I just tried to find the right balance between test coverage and time invested.