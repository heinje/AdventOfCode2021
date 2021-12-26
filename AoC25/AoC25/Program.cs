using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC25
{
    class Program
    {
        static void Main(string[] args)
        {
            var filename = "TestInput.txt";
            task1(filename);
            task2(filename);
        }

        private static void task1(string filename)
        {
            Console.WriteLine("Task 1:");
            Console.WriteLine("=======");

            Console.WriteLine();
        }

        private static void task2(string filename)
        {
            Console.WriteLine("Task 2:");
            Console.WriteLine("=======");

            Console.WriteLine();
        }

        private static IEnumerable<int> readDataFromFile(string fileName)
        {
            var inputFile = new FileInfo(fileName);
            using var fileStream = inputFile.OpenRead();
            using var fileReader = new StreamReader(fileStream);
            while (!fileReader.EndOfStream)
            {
                var number = fileReader.ReadLine();
                if (int.TryParse(number, out var value))
                {
                    yield return value;
                }
            }
        }
    }
}
