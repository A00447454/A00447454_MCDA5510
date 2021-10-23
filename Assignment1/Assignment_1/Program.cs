using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace Assignment_1
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int count = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();

            

            //(@"/Users/sunarithakaranth/Desktop/MCDA5510_Assignments-master/Sample Data/");
            

            listFilesFromDirectory(@"/Users/sunarithakaranth/Desktop/A00447454_MCDA5510/Assignment1/Assignment_1/SampleData/", count);
            //Console.WriteLine("\n\ncount"+ count);

            //Console.Read();

            sw.Stop();
            Console.WriteLine("It took {0} seconds to run ", sw.Elapsed);
            //Console.WriteLine("");

        }
        static void getSubDirectories(string workingDirectory)
            {
                string[] subDirectories = Directory.GetDirectories(workingDirectory,"*",SearchOption.AllDirectories);


                foreach (string subDirectory in subDirectories)
                {
                    
                    Console.WriteLine(subDirectory);
                }
            }

        static void listFilesFromDirectory(string workingDirectory, int count)
        {
            
            string[] filePaths = Directory.GetFiles(workingDirectory, "*", SearchOption.AllDirectories);
            foreach (string filePath in filePaths)
            {
               
                Console.WriteLine(Path.GetFileName(filePath));
                count++;
            }
            Console.WriteLine("\n\ncount" + count);
            //return count;
        }


    }
}
