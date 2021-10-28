using System;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using log4net;
using Microsoft.VisualBasic.FileIO;



namespace Assignment1

{


    class MainClass
    {
        public static string dir = @"C:\Users\Sunaritha\Documents\MCDA\5510 Software Dev in Bus Envir\MCDA5510_Assignments-master\MCDA5510_Assignments-master\Sample Data\Sample Data\";
        public static string output = dir + @"..\..\Output\Output.csv";
        public static string data = dir + @"SampleFiles\Sample Data\";
        public static string logFilePath = dir + @"..\..\Logs\log.txt";
        public static int skippedRows = 0;
        public static int validRows = 0;

        public static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            // CsvParser Parser = new CsvParser();
            File.AppendAllText(output, "First Name,Last Name,Street Number,Street,City," +
                "Province,Postal Code,Country,Phone Number,email Address,Date\n");
            Walk(dir);


            sw.Stop();



            CsvParser.Log("It took seconds to run " + sw.Elapsed);
            CsvParser.Log("skippedRows:" + skippedRows + "\nvalidRows: " + validRows);
        }




        private static void Walk(string path)

        {
            string[] list = Directory.GetDirectories(path);
            if (list == null)
            {
                return;
            }
            foreach (string subDirectory in list)
            {
                Walk(subDirectory);
                //Console.WriteLine(subDirectory);
            }


            string[] file = Directory.GetFiles(path);
            CsvParser Parser = new CsvParser();

            foreach (string filepath in file)
            {
                if (Path.GetExtension(filepath) == ".csv")
                {
                    //Console.WriteLine(filepath);
                    Parser.Parse(filepath);
                }

            }


        }
        public class CsvParser
        {
            private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            public void Parse(String fileName)
            {

                try
                {
                    //using (StreamWriter fileStreamWriter = File.AppendText(output))
                    using (TextFieldParser parser = new TextFieldParser(fileName))
                    {

                        parser.TextFieldType = FieldType.Delimited;
                        parser.SetDelimiters(",");



                        //Process row
                        while (!parser.EndOfData)
                        {

                            string row = "";
                            int skip = 0;

                            string[] fields = parser.ReadFields();

                            if (fields[0] == "First Name")
                            {
                                continue;
                            }


                            foreach (string field in fields)
                            {

                                row += field + ",";

                                if (field == null || field == "")
                                {
                                    skippedRows++;
                                    skip = 1;

                                    string message = "Skipped row: " + row;
                                    Log("Row Skipped in " + fileName);

                                    break;
                                }
                            }

                            if (skip == 0)
                            {
                                string[] splitFileName = fileName.Split('\\');
                                string date = splitFileName[splitFileName.Length - 4] + "/" + splitFileName[splitFileName.Length - 3] + "/" + splitFileName[splitFileName.Length - 2];
                                row = row.Remove(row.Length - 1);
                                row += "," + date;
                                File.AppendAllText(output, row + "\n");

                                validRows++;
                            }

                        }

                    }
                }
                catch (IOException ioe)
                {
                    Log(ioe.StackTrace);
                }
            }
            public static void addRecord(string record)
            {
                //  try
                //    {
                using (StreamWriter file = new StreamWriter(@"/Users/sunarithakaranth/Desktop/A00447454_MCDA5510/Assignment1/Assignment_1/SampleData/output.csv", true))
                {
                    file.WriteLine(record);
                }
                //}

            }

            //      new StreamWriter(Directory.GetCurrentDirectory() + "/output.csv", true))
            //       {
            // file.WriteLine(record);

            //   }




            public static void Log(string Message)
            {
                //var logRepo = LogManager.GetRepository(Assembly.GetEntryAssembly());
                //XmlConfigurator.Configure(logRepo, new FileInfo("log4net.config"));

                //log.Error(Message);

                File.AppendAllText(logFilePath, Message + "\n");


            }
        }
        public class Exceptions
        {


            // static void Main()
            //  {
            //    var sw = OpenStream(@".\sampleFile.csv");
            //  if (sw is null)
            //    return;
            //      sw.WriteLine("This is the first line.");
            //     sw.WriteLine("This is the second line.");
            //     sw.Close();
            //}

            static StreamWriter OpenStream(string path)
            {
                if (path is null)
                {
                    Console.WriteLine("You did not supply a file path.");
                    return null;
                }

                try
                {
                    var fs = new FileStream(path, FileMode.CreateNew);
                    return new StreamWriter(fs);
                }
                catch (FileNotFoundException)
                {
                    CsvParser.Log("The file or directory cannot be found.");
                }
                catch (DirectoryNotFoundException)
                {
                    CsvParser.Log("The file or directory cannot be found.");
                }
                catch (DriveNotFoundException)
                {
                    CsvParser.Log("The drive specified in 'path' is invalid.");
                }
                catch (PathTooLongException)
                {
                    CsvParser.Log("'path' exceeds the maxium supported path length.");
                }
                catch (UnauthorizedAccessException)
                {
                    CsvParser.Log("You do not have permission to create this file.");
                }
                catch (IOException e) when ((e.HResult & 0x0000FFFF) == 32)
                {
                    CsvParser.Log("There is a sharing violation.");
                }
                catch (IOException e) when ((e.HResult & 0x0000FFFF) == 80)
                {
                    CsvParser.Log("The file already exists.");
                }
                catch (IOException e)
                {
                    CsvParser.Log($"An exception occurred:\nError code: " +
                                      $"{e.HResult & 0x0000FFFF}\nMessage: {e.Message}");
                }
                return null;
            }
        }

    }

}