using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AoC16
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = new List<string>{
                "TestInput11.txt",
                "TestInput12.txt",
                "TestInput13.txt",
                "TestInput14.txt",
                "TestInput21.txt",
                "TestInput22.txt",
                "TestInput23.txt",
                "TestInput24.txt",
                "TestInput25.txt",
                "TestInput26.txt",
                "TestInput27.txt",
                "TestInput28.txt",
                "Input.txt",
            };

            foreach (var filename in files)
            {
                Console.WriteLine($"=== File: {filename} ===");
                Console.WriteLine();

                task1(filename);
                task2(filename);

                Console.WriteLine();
            }
        }

        private static void task1(string filename)
        {
            Console.WriteLine("Task 1:");
            Console.WriteLine("=======");

            var input = PackageParser.Parse(readDataFromFile(filename));

            Console.WriteLine($"Version sum: {input.GetVersionSum()}");

            Console.WriteLine();
        }

        private static void task2(string filename)
        {
            Console.WriteLine("Task 2:");
            Console.WriteLine("=======");

            var input = PackageParser.Parse(readDataFromFile(filename));

            Console.WriteLine($"Value: {input.GetValue()}");

            Console.WriteLine();
        }

        private static string readDataFromFile(string fileName)
        {
            var inputFile = new FileInfo(fileName);
            using var fileStream = inputFile.OpenRead();
            using var fileReader = new StreamReader(fileStream);
            return HexToBinaryString(fileReader.ReadLine());
        }

        private static string HexToBinaryString(string hexString)
        {
            return string.Join("", hexString.ToCharArray().Select(HexToBinaryValue));
        }

        private static string HexToBinaryValue(char hexValue)
        {
            var binary = byte.Parse("0" + hexValue, NumberStyles.HexNumber);
            return Convert.ToString(binary, 2).PadLeft(4, '0');
        }
    }
}
