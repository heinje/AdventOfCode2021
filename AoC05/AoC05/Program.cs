using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AoC05
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

            var entries = readDataFromFile(filename);
            var points = new Dictionary<Point, int>();
            foreach (var point in entries
                .Where(entry => entry.IsHorizontal || entry.IsVertical)
                .SelectMany(entry => entry.getPointsOnLine()))
            {
                if (!points.ContainsKey(point))
                {
                    points[point] = 1;
                }
                else
                {
                    points[point]++;
                }
            }

            var crossingPoints = points.Count(point => point.Value > 1);
            Console.WriteLine($"{crossingPoints} crossing points");
            
            Console.WriteLine();
        }

        private static void task2(string filename)
        {
            Console.WriteLine("Task 2:");
            Console.WriteLine("=======");

            var entries = readDataFromFile(filename);
            var points = new Dictionary<Point, int>();
            foreach (var point in entries
                .SelectMany(entry => entry.getPointsOnLine()))
            {
                if (!points.ContainsKey(point))
                {
                    points[point] = 1;
                }
                else
                {
                    points[point]++;
                }
            }

            var crossingPoints = points.Count(point => point.Value > 1);
            Console.WriteLine($"{crossingPoints} crossing points");

            Console.WriteLine();
        }

        private static IEnumerable<Entry> readDataFromFile(string fileName)
        {
            var inputFile = new FileInfo(fileName);
            using var fileStream = inputFile.OpenRead();
            using var fileReader = new StreamReader(fileStream);
            while (!fileReader.EndOfStream)
            {
                yield return new Entry(fileReader.ReadLine());
            }
        }
    }
}
