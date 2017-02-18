# Summary (where do I start?)

## Content

* Proposed solution source code with C# with Visual Studio 2015.
* the executable can be found at the root of this repo, within `AddressFileProcessing.zip`  	

## Prerequisites

* MS .net Framework 4.6.1
* Git to get the code (optional)
* Visual Studio 2013 or 2015 to open and edit it (optional)

## Run the executable

The executable (AddressFileProcessing.exe) should be use with 3 paramaters: 

```
AddressFileProcessing <inputfile> <namefile> <addressesfile>
```

The input file must be located in the same folder as the executable. The outputs files, will be created in this folder too, given that program executed successfully.

## Run in Debug Mode (Visual Studio 2015)

* Open the .sln file
* Run the main project (AddressFileProcessing) as debug (the file included, data.csv will be automatically copied to the /bin folder)

## Output

command line
```
> AddressFileProcessing.exe data.csv names.csv addresses.csv

This file will be read: data.csv
Processing ...
Processing done.
The output files for names and addresses has been created: names.csv and addresses.csv.
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
   /Reader : component to read a text file and a generic interface for reading content
   /Writer : component to write a text file and a generic interface for writing content

/Models : models representing what is read and persisted to our files 

/Parsing : helpers to parse the input file rows
  
/Storage : contains the accumulator ( = repository to store the content read and run the aggregations on it)

/Processing : contains the processor that orcherstrate and perform a run and generate the results


Program.cs : the entry point to run the processing
```

# Design (why it is done this way?)

## Choices / decision / assumptions

In order to ease the testing I tried to decompose the app in small independant components (by responsability) as much as I could. 
That way I could create a component, test it, then move to the next one, ensuring that they all work properly.

The file `readers` and `writers` both implement an interface. At a first glance, it might not seem necessary, but it
helps for the unit tests; instead of having to provide the reader/writer concrete class to the `processor`, a mocked reader/writer 
with in-memory dataset could be created and passed to the `processor`. That way we can inject in-memory data from the `reader` and 
check that the content is generated properly on write from the `writer`. Also if we wanted to extend this application to use others 
medium of storage we could by simply creating new reader/writer (without changing anything else).

The phonenumber fields has been voluntarly ommited as we don't use it in the application.


# Testing (does it actually work?)

## Summary

`NUnit` is used as a testing framework. 
`Moq` as subtitution/mocking/faking tool.

The critical components has been unit tested :

* Parser : to check that it fails when required and pass when result is valid.
* Accumulator : to verify that the query to calculate the _names frequencies_ & _address_ follows the instruction.
* Processor : just to verify that the example given in the instructions works as defined in them.

More components/methods could have been tested and more tests cases could have been added for the tested components, especially to test all the edge cases.