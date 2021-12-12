using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AoC09
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

            var heightMap = readDataFromFile(filename);
            var localMinima = getLocalMinima(heightMap).ToList();

            print(heightMap, localMinima);

            var result = localMinima.Sum(localMinimum => heightMap[localMinimum.Y][localMinimum.X] + 1);

            Console.WriteLine();
            Console.WriteLine($"Result: {result}");

            Console.WriteLine();
        }

        private static void task2(string filename)
        {
            Console.WriteLine("Task 2:");
            Console.WriteLine("=======");

            var heightMap = readDataFromFile(filename);
            var localMinima = getLocalMinima(heightMap);
            var basins = localMinima.Select(localMinimum => getBasin(heightMap, localMinimum));

            print(heightMap, basins);

            var result = 1;
            foreach (var basin in basins.OrderByDescending(basin => basin.Count).Take(3))
            {
                result *= basin.Count;
            }
            Console.WriteLine();
            Console.WriteLine($"Result: {result}");

            Console.WriteLine();
        }

        private static int[][] readDataFromFile(string fileName)
        {
            var inputFile = new FileInfo(fileName);
            var rows = new List<int[]>();
            using var fileStream = inputFile.OpenRead();
            using var fileReader = new StreamReader(fileStream);
            while (!fileReader.EndOfStream)
            {
                var line = fileReader.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    rows.Add(line.Select(character => int.Parse(character.ToString())).ToArray());
                }
            }

            return rows.ToArray();
        }

        private static IEnumerable<Point> getLocalMinima(int[][] heightMap)
        {
            for (var y = 0; y < heightMap.Length; y++)
            {
                for (var x = 0; x < heightMap[y].Length; x++)
                {
                    if (x > 0 && heightMap[y][x] >= heightMap[y][x - 1])
                        continue;

                    if (x < heightMap[y].Length - 1 && heightMap[y][x] >= heightMap[y][x + 1])
                        continue;

                    if (y > 0 && heightMap[y][x] >= heightMap[y - 1][x])
                        continue;

                    if (y < heightMap.Length - 1 && heightMap[y][x] >= heightMap[y + 1][x])
                        continue;

                    yield return new Point(x, y);
                }
            }
        }

        private static List<Point> getBasin(int[][] heightMap, Point minimum)
        {
            var basin = new List<Point> {minimum};
            for (var index = 0; index < basin.Count; index++)
            {
                var point = basin[index];

                foreach (var adjacent in new List<Point>
                {
                    new Point(point.X - 1, point.Y),
                    new Point(point.X + 1, point.Y),
                    new Point(point.X, point.Y - 1),
                    new Point(point.X, point.Y + 1),
                })
                {
                    if (adjacent.Y >= 0
                        && adjacent.Y < heightMap.Length
                        && adjacent.X >= 0
                        && adjacent.X < heightMap[adjacent.Y].Length
                        && heightMap[adjacent.Y][adjacent.X] < 9
                        && !basin.Contains(adjacent))
                    {
                        basin.Add(adjacent);
                    }
                }
            }

            return basin;
        }

        private static void print(int[][] heightMap, List<Point> localMinima)
        {
            var defaultColor = Console.ForegroundColor;
            for (var y = 0; y < heightMap.Length; y++)
            {
                for (var x = 0; x < heightMap[y].Length; x++)
                {
                    Console.ForegroundColor = localMinima.Contains(new Point(x, y)) ? ConsoleColor.Cyan : defaultColor;
                    Console.Write(heightMap[y][x]);
                }

                Console.WriteLine();
            }

            Console.ForegroundColor = defaultColor;
        }

        private static void print(int[][] heightMap, IEnumerable<List<Point>> basins)
        {
            var defaultColor = Console.ForegroundColor;
            var colorMap = new int[heightMap.Length][];
            for (var y = 0; y < heightMap.Length; y++)
            {
                colorMap[y] = new int[heightMap[y].Length];
            }

            int basinNumber = 0;
            foreach (var basin in basins)
            {
                basinNumber++;
                foreach (var point in basin)
                {
                    colorMap[point.Y][point.X] = basinNumber;
                }
            }
            for (var y = 0; y < heightMap.Length; y++)
            {
                for (var x = 0; x < heightMap[y].Length; x++)
                {
                    Console.ForegroundColor = getColor(colorMap[y][x], defaultColor);
                    Console.Write(heightMap[y][x]);
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = defaultColor;
        }

        private static ConsoleColor getColor(int value, ConsoleColor defaultColor)
        {
            if (value == 0)
            {
                return defaultColor;
            }
            switch (value % 12)
            {
                case 1:
                    return ConsoleColor.DarkBlue;
                case 2:
                    return ConsoleColor.DarkGreen;
                case 3:
                    return ConsoleColor.DarkCyan;
                case 4:
                    return ConsoleColor.DarkRed;
                case 5:
                    return ConsoleColor.DarkMagenta;
                case 6:
                    return ConsoleColor.DarkYellow;
                case 7:
                    return ConsoleColor.Blue;
                case 8:
                    return ConsoleColor.Green;
                case 9:
                    return ConsoleColor.Cyan;
                case 10:
                    return ConsoleColor.Red;
                case 11:
                    return ConsoleColor.Magenta;
                default:
                    return ConsoleColor.Yellow;
            }
        }
    }
}
