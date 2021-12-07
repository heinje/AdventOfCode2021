using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC06
{
    class Program
    {
        static void Main(string[] args)
        {
            var filename = "Input.txt";
            task1(filename);
            task2(filename);
        }

        private static void task1(string filename)
        {
            Console.WriteLine("Task 1:");
            Console.WriteLine("=======");

            var fish = new FishTank(readNumbersFromFile(filename));
            Console.WriteLine($"Day 00: {fish.Count} fish");
            for (var day = 0; day < 80; day++)
            {
                fish.CalculateNextDay();

                if (day % 20 == 19)
                {
                    Console.WriteLine($"Day {day+1}: {fish.Count} fish");
                }
            }

            Console.WriteLine();
        }

        private static void task2(string filename)
        {
            Console.WriteLine("Task 2:");
            Console.WriteLine("=======");

            var fish = new FishTank(readNumbersFromFile(filename));
            Console.WriteLine($"Day 000: {fish.Count} fish");
            for (var day = 0; day < 256; day++)
            {
                fish.CalculateNextDay();

                if (day % 64 == 63)
                {
                    Console.WriteLine($"Day {day + 1:D3}: {fish.Count} fish");
                }
            }

            Console.WriteLine();
        }

        private static IEnumerable<int> readNumbersFromFile(string fileName)
        {
            var inputFile = new FileInfo(fileName);
            using var fileStream = inputFile.OpenRead();
            using var fileReader = new StreamReader(fileStream);
            var input = fileReader.ReadLine();
            return input.Split(',').Select(int.Parse);
        }
    }
}
