using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AoC17
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

            var input = readDataFromFile(filename);

            var results = new Dictionary<Point, int>();
            for (var x = 1; x < input.Maximum.X / 2; x++)
                for (var y = 1; y < -input.Minimum.Y; y++)
                {
                    var startingVelocity = new Point(x, y);
                    var velocity = new Size(x, y);
                    var coordinates = new Point(0, 0);
                    var highestAltitude = 0;
                    while (!input.HasMissed(coordinates))
                    {
                        coordinates = Point.Add(coordinates, velocity);
                        if (coordinates.Y > highestAltitude)
                        {
                            highestAltitude = coordinates.Y;
                        }
                        if (input.IsWithin(coordinates))
                        {
                            results[startingVelocity] = highestAltitude;
                            continue;
                        }
                        velocity.Width = Math.Max(0, velocity.Width - 1);
                        velocity.Height -= 1;
                    }
                }

            foreach(var result in results.OrderByDescending(entry => entry.Value).Take(10))
            {
                Console.WriteLine($"{result.Key}: {result.Value}");
            }

            Console.WriteLine();
        }

        private static void task2(string filename)
        {
            Console.WriteLine("Task 2:");
            Console.WriteLine("=======");

            var input = readDataFromFile(filename);

            var results = new Dictionary<Point, int>();
            for (var x = 1; x <= input.Maximum.X; x++)
                for (var y = input.Minimum.Y; y < -input.Minimum.Y; y++)
                {
                    var startingVelocity = new Point(x, y);
                    var velocity = new Size(x, y);
                    var coordinates = new Point(0, 0);
                    var highestAltitude = 0;
                    while (!input.HasMissed(coordinates))
                    {
                        coordinates = Point.Add(coordinates, velocity);
                        if (coordinates.Y > highestAltitude)
                        {
                            highestAltitude = coordinates.Y;
                        }
                        if (input.IsWithin(coordinates))
                        {
                            results[startingVelocity] = highestAltitude;
                            continue;
                        }
                        velocity.Width = Math.Max(0, velocity.Width - 1);
                        velocity.Height -= 1;
                    }
                }

            Console.WriteLine($"{results.Count} results");

            Console.WriteLine();
        }

        private static Input readDataFromFile(string fileName)
        {
            var inputFile = new FileInfo(fileName);
            var pattern = new Regex(@"^target area: x=(\d+)\.\.(\d+), y=(-?\d+)..(-?\d+)");
            using var fileStream = inputFile.OpenRead();
            using var fileReader = new StreamReader(fileStream);
            var match = pattern.Match(fileReader.ReadLine());

            var x1 = int.Parse(match.Groups[1].Value);
            var x2 = int.Parse(match.Groups[2].Value);
            var y1 = int.Parse(match.Groups[3].Value);
            var y2 = int.Parse(match.Groups[4].Value);

            return new Input(x1, x2, y1, y2);
        }
    }
}
