using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OutSuranceTest.Worker;

namespace OutSuranceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating Text Files...");
            System.Threading.Thread.Sleep(50);
            CreateSortedDataFilesFromCSVFile d = new CreateSortedDataFilesFromCSVFile();
            d.CreateSortedDataFiles();
            System.Threading.Thread.Sleep(200);
            Console.WriteLine("Creating Text Files Completed...Please check the project folder.");
            Console.ReadLine();
        }
    }
}
