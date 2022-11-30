# Designer Brands Take Home Assessment
## Problem
* Parse a comma-seperated text file with `types` and `products` and print a report that displays the number of products that make up each type. When applicable, display the price-range for those products.
## Solution
The application takes a file path as a command-line argument. 
* **Note: This applicaiton reads the command-line argument *relative* to the current user's directory.**
### Tech Stack
This project is written in C# using .NET 6. As a compiled language, I have included versions of this program that target MacOS and Windows. 
* Please make sure you are using the version that matches your operating system.

The application's data is stored in memory during processing. Nothing is saved to a database or persisted beyond runtime.
## Application Design 
This application uses OOP principles for organization. `Types` and `Products` are treated as objects and the program processes data by acting on these data objects and other objects built up around them.
### Data Modeling
I decided to model `Types` and `Products` as separate data objects. This allowed me to parse the processed data to these object types during the applications runtime.

As such, Types.cs and Products.cs merely store the information provided by the data.txt file. This structure allows for `Types` and `Products` to be manipulated programitically. If a new `Type` is included in the source file, for example, this application has the ability to include that into the List of Types.

* As written, however, any additional type would not be utilized to process the `Products` in the data file.

Similarly, `Products` are parsed to a data object, and can be used to process many individual products. 

With those two data objects holding the base-level information, I decided to create more complex data objects that could store combinations of data for the creation of reports. 

`FileInput.cs` holds a list of types and a list of products. This object class is used by the reporting functions to iterate through all the included types and products from the data file. 

`ReportData.cs` holds the information needed for the report itself--number of products, price range, quantity, etc. Once the application sorts the products by type, it uses the `ReportData.cs` class to process the report logic by creating an enumerable list of type ReportData. 

### Class Definitions
The logic for the application is broken into three classes:

1. `Program.cs` is the main class for running C# console applications. This class is used to start the application, take the command-line argument, and call the required classes.
1. `FileAccess.cs` is called from Program.cs where it is passed the command-line argument. This class parses the data file and saves the information in the previously discussed data models. 
1. `RunReport.cs` is called once FileAccess has parsed the data file. Program.cs passes the file information to RunReport.cs for processing and report printing.

I decided to structure the application this way to separate concerns. Program.cs is only responsible for running the needed code and passing needed parameters. FileAccess is the only place to directly act on the file specified at runtime. RunReport is only responsible for processing the data to print the report. 

This application structure would allow for expansion and refinement in the future, while 
#### Program.CS
    public class Program
    {
        static void Main(string[] args)
        {
            FileAccess fileAccess = new FileAccess();
            RunReport runReport = new RunReport();

            FileInput fileInput = fileAccess.ReadInputFile(args[0]);
            runReport.ProcessInput(fileInput);
        }
    }
### What I would've done differently
1. Avoid `void` methods to make testing easier
1. Spend more time for refactoring after testing
1. Add error handling

#### 1. Avoid `void` methods to make testing easier
Because I haven't written a console application in over a year, I was a bit rusty in how best to architect this solution to make it easy for testing. When I learned C#, I was taught to use `void` methods for processing data without needing to return a specific data type. That process makes writing console applications faster, but it complicates testing because nothing is returned by the method. There are two ways around this: 
1. Test something that is changed by the method--like a state variable.
1. Refactor the method to use a return type that can be easily tested, like a bool, or another value that doesn't change the core logic of the program.

#### 2. Spend more time for refactoring after testing
I refactored a few methods after building out a few basic tests, but I ran out of time to do a more complete refactoring. Beside making the code more testable, I would want to focus on cleaning the code to make sure that variable and method names made sense and indicated what they do. I would also want to focus on further organizing the application's file structure. It is fine enough now, but I'd want to better utilize folders to clarify structure. If this application needed to grow, creating a structure for business logic classes would be a priority. 

#### 3. Add error handling
Currently, this application uses little error handling. It will throw an `out of index exception` if no command-line arguments are submitted at runtime to point to the data file. Otherwise, I'm checking for empty object lists by explicitly looking for a `.Count == 0`, but this is crude and doesn't check for `null` values. Given more time, error handling would need to be a priority.  

## How to Run
Using a bash terminal, you will run the program by calling  `./DesignerBrands` followed by the location of the data file relative to your user directory. 

For example, on MacOS the input would be as follows:

    ./DesignerBrands Desktop/data.txt

This command works on MacOS and using a bash based terminal in Windows (like GitBash). I have not tested the commands needed to use Powershell or Command Prompt in Windows.     
### Data.txt Output
Using the included Data.txt information, the application outputs:

    Clearance Price: 2 products @ 39.98-49.98
    Normal Price: 1 products @ 49.99
    Price in Cart: 0 products
