using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC13
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
            var dots = fold(input.Dots, input.FoldingInstructions.First());
            Console.WriteLine($"{dots.Distinct().Count()} dots");

            Console.WriteLine();
        }

        private static void task2(string filename)
        {
            Console.WriteLine("Task 2:");
            Console.WriteLine("=======");

            var input = readDataFromFile(filename);
            IEnumerable<Point> dots = input.Dots;
            foreach (var foldingInstruction in input.FoldingInstructions)
            {
                dots = fold(dots, foldingInstruction);
            }

            print(dots.ToList());

            Console.WriteLine();
        }

        private static void print(List<Point> dots)
        {
            var maxX = dots.Max(dot => dot.X);
            var maxY = dots.Max(dot => dot.Y);
            for (var y = 0; y <= maxY; y++)
            {
                for (var x = 0; x <= maxX; x++)
                {
                    Console.Write(dots.Contains(new Point(x, y)) ? "#" : " ");
                }

                Console.WriteLine();
            }
        }

        private static IEnumerable<Point> fold(IEnumerable<Point> points, FoldingInstruction foldingInstruction)
        {
            var newPoints = new List<Point>();
            foreach (var point in points)
            {
                if (foldingInstruction.Axis == EnmAxis.X)
                {
                    yield return point.X < foldingInstruction.Value
                        ? point
                        : new Point(2 * foldingInstruction.Value - point.X, point.Y);
                }
                else
                {
                    yield return point.Y < foldingInstruction.Value
                        ? point
                        : new Point(point.X, 2 * foldingInstruction.Value - point.Y);
                }
            }
        }

        private static Input readDataFromFile(string fileName)
        {
            var inputFile = new FileInfo(fileName);
            var input = new Input();
            var patternDot = new Regex(@"^(\d+),(\d+)$");
            var patternFolding = new Regex(@"^fold along ([xy])=(\d+)$");
            using var fileStream = inputFile.OpenRead();
            using var fileReader = new StreamReader(fileStream);
            while (!fileReader.EndOfStream)
            {
                var line = fileReader.ReadLine();
                var dotMatch = patternDot.Match(line);
                if(dotMatch.Success)
                {
                    var x = int.Parse(dotMatch.Groups[1].Value);
                    var y = int.Parse(dotMatch.Groups[2].Value);
                    input.Dots.Add(new Point(x, y));
                    continue;
                }

                var foldingMatch = patternFolding.Match(line);
                if (foldingMatch.Success)
                {
                    var axis = Enum.Parse<EnmAxis>(foldingMatch.Groups[1].Value, true);
                    var value = int.Parse(foldingMatch.Groups[2].Value);
                    input.FoldingInstructions.Add(new FoldingInstruction(axis, value));
                }
            }

            return input;
        }
    }
}
