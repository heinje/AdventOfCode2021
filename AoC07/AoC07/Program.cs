using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC07
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

            calculateOptimum(filename, fuelConsumptionEasy);

            Console.WriteLine();
        }

        private static void task2(string filename)
        {
            Console.WriteLine("Task 2:");
            Console.WriteLine("=======");

            calculateOptimum(filename, fuelConsumptionComplicated);

            Console.WriteLine();
        }

        private static void calculateOptimum(string filename, Func<int, int, int> fuelCalculation)
        {
            var numbers = readNumbersFromFile(filename).ToList();
            var average = calculateAverage(numbers);
            var median = calculateMedian(numbers);

            var start = numbers.Min();
            var end = numbers.Max();

            var minimumFuel = int.MaxValue;
            var meetingPoint = 0;
            for (var point = start; point <= end; point++)
            {
                var fuel = calculateFuel(numbers, point, fuelCalculation);
                if (fuel < minimumFuel)
                {
                    minimumFuel = fuel;
                    meetingPoint = point;
                }
            }

            Console.WriteLine($"Average: {average} ({calculateFuel(numbers, (int)average, fuelCalculation)})");
            Console.WriteLine($"Median : {median} ({calculateFuel(numbers, median, fuelCalculation)})");
            Console.WriteLine($"Optimum: {meetingPoint} ({calculateFuel(numbers, meetingPoint, fuelCalculation)})");
        }

        private static int calculateFuel(List<int> numbers, int meetingPoint, Func<int,int,int> fuelCalculation)
        {
            return numbers.Sum(number => fuelCalculation(number, meetingPoint));
        }

        private static int fuelConsumptionEasy(int position1, int position2)
        {
            return Math.Abs(position1 - position2);
        }

        private static int fuelConsumptionComplicated(int position1, int position2)
        {
            var diff = Math.Abs(position1 - position2);
            return (diff + 1) * diff / 2;
        }

        private static int calculateAverage(List<int> numbers)
        {
            return (int)Math.Round(numbers.Average());
        }

        private static int calculateMedian(List<int> numbers)
        {
            var sorted = numbers.OrderBy(entry => entry).ToList();
            if (sorted.Count % 2 == 1)
            {
                return sorted[(sorted.Count - 1) / 2];
            }
            else
            {
                return (int)Math.Round((double)(sorted[sorted.Count / 2] + sorted[sorted.Count / 2 - 1]) / 2);
            }
        }

        private static IEnumerable<int> readNumbersFromFile(string fileName)
        {
            var inputFile = new FileInfo(fileName);
            using var fileStream = inputFile.OpenRead();
            using var fileReader = new StreamReader(fileStream);
            return fileReader.ReadLine()?.Split(',').Select(int.Parse);
        }
    }
}
