# Assignment1
The assignment is to read a bunch of data files in csv format and write all the valid rows into one file named "output.csv". Meanwhile, counting the number of all the valid rows.
Similarly, all the rows with null values or missing data is to be exempted from writing into output file and instead logged into a log.txt file where all the skipped rows are written and total number of skipped rows are counted.
Total time needed to execute the program is also noted.
Data columns in output file: First Name, Last Name, Street Number, Street, City, Province, Country, Postal Code, Phone Number, email Addres

## Given functions:
DirWalker: to walk through the directory(SampleData) and sub directories in the given path and get all the csv files.
SimpleCSVParser: parse through each row of all csv data files
Exceptions:  catch the exceptions.


Main Code: Program.cs
Logs: Logs folder
Output: output folder
