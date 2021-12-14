using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AoC11
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

            var numbers = readDataFromFile(filename);
            var flashes = 0;
            for (var step = 0; step < 100; step++)
            {
                flashes += calculateStep(numbers);
            }

            Console.WriteLine($"{flashes} flashes");

            Console.WriteLine();
        }

        private static void task2(string filename)
        {
            Console.WriteLine("Task 2:");
            Console.WriteLine("=======");

            var numbers = readDataFromFile(filename);
            var numberCount = numbers.Sum(line => line.Length);
            var step = 0;
            var lastNumberOfFlashes = 0;
            while (step < 100000 && lastNumberOfFlashes < numberCount)
            {
                lastNumberOfFlashes = calculateStep(numbers);
                step++;
            }

            Console.WriteLine($"Step {step}: {lastNumberOfFlashes} flashes");

            Console.WriteLine();
        }

        private static int calculateStep(int[][] numbers)
        {
            var flashingPoints = increaseByOne(numbers);
            var flashes = calculateFlashes(numbers, flashingPoints);
            setFlashedToZero(numbers);

            return flashes;
        }

        private static int calculateFlashes(int[][] numbers, Stack<Point> flashingPoints)
        {
            var flashes = 0;
            while (flashingPoints.Count > 0)
            {
                var flashingPoint = flashingPoints.Pop();
                flashes++;
                foreach (var point in getAdjacentPoints(flashingPoint))
                {
                    if (point.Y >= 0
                        && point.Y < numbers.Length
                        && point.X >= 0
                        && point.X < numbers[point.Y].Length)
                    {
                        numbers[point.Y][point.X]++;
                        if (numbers[point.Y][point.X] == 10)
                        {
                            flashingPoints.Push(point);
                        }
                    }
                }
            }

            return flashes;
        }

        private static Stack<Point> increaseByOne(int[][] numbers)
        {
            var flashingPoints = new Stack<Point>();
            for (var y = 0; y < numbers.Length; y++)
            for (var x = 0; x < numbers[y].Length; x++)
            {
                numbers[y][x]++;
                if (numbers[y][x] > 9)
                {
                    flashingPoints.Push(new Point(x, y));
                }
            }

            return flashingPoints;
        }

        private static void setFlashedToZero(int[][] numbers)
        {
            for (var y = 0; y < numbers.Length; y++)
            for (var x = 0; x < numbers[y].Length; x++)
            {
                if (numbers[y][x] > 9)
                {
                    numbers[y][x] = 0;
                }
            }
        }

        private static IEnumerable<Point> getAdjacentPoints(Point flashingPoint)
        {
            yield return new Point(flashingPoint.X - 1, flashingPoint.Y - 1);
            yield return new Point(flashingPoint.X, flashingPoint.Y - 1);
            yield return new Point(flashingPoint.X + 1, flashingPoint.Y - 1);
            yield return new Point(flashingPoint.X - 1, flashingPoint.Y);
            yield return new Point(flashingPoint.X + 1, flashingPoint.Y);
            yield return new Point(flashingPoint.X - 1, flashingPoint.Y + 1);
            yield return new Point(flashingPoint.X, flashingPoint.Y + 1);
            yield return new Point(flashingPoint.X + 1, flashingPoint.Y + 1);
        }

        private static int[][] readDataFromFile(string fileName)
        {
            var inputFile = new FileInfo(fileName);
            var values = new List<int[]>();
            using var fileStream = inputFile.OpenRead();
            using var fileReader = new StreamReader(fileStream);
            while (!fileReader.EndOfStream)
            {
                var line = fileReader.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    values.Add(line.Select(digit => int.Parse(digit.ToString())).ToArray());
                }
            }

            return values.ToArray();
        }
    }
}
