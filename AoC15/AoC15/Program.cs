using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AoC15
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

            var map = readDataFromFile(filename);

            var distances = calculateDistances(map);

            var minimumDistance = distances[distances.GetLength(0) - 1, distances.GetLength(1) - 1];

            Console.WriteLine($"Min.: {minimumDistance}");

            Console.WriteLine();
        }

        private static void task2(string filename)
        {
            Console.WriteLine("Task 2:");
            Console.WriteLine("=======");

            var map = calculateBiggerMap(readDataFromFile(filename),5);

            var distances = calculateDistances(map);

            var minimumDistance = distances[distances.GetLength(0) - 1, distances.GetLength(1) - 1];

            Console.WriteLine($"Min.: {minimumDistance}");

            Console.WriteLine();
        }

        private static int[,] calculateDistances(int[,] map)
        {
            var distances = new int[map.GetLength(0), map.GetLength(1)];
            var toCheck = new Queue<Point>();
            toCheck.Enqueue(new Point(0, 0));

            while (toCheck.TryDequeue(out var point))
            {
                foreach (var neighbour in getNeighbours(point, map))
                {
                    var newDistance = distances[point.X, point.Y] + map[neighbour.X,neighbour.Y];
                    if (distances[neighbour.X, neighbour.Y] == 0
                        || distances[neighbour.X, neighbour.Y] > newDistance)
                    {
                        distances[neighbour.X, neighbour.Y] = newDistance;
                        toCheck.Enqueue(neighbour);
                    }
                }
            }

            return distances;
        }

        private static IEnumerable<Point> getNeighbours(Point point, int[,] map)
        {
            if (point.X > 0)
            {
                yield return new Point(point.X - 1, point.Y);
            }

            if (point.X < map.GetLength(0) - 1)
            {
                yield return new Point(point.X + 1, point.Y);
            }
            
            if (point.Y > 0)
            {
                yield return new Point(point.X, point.Y - 1);
            }

            if (point.Y < map.GetLength(1) - 1)
            {
                yield return new Point(point.X, point.Y + 1);
            }
        }

        private static int[,] calculateBiggerMap(int[,] originalMap, int sizeMultiplier)
        {
            var newMap = new int[originalMap.GetLength(0) * sizeMultiplier, originalMap.GetLength(1) * sizeMultiplier];
            for (var x = 0; x < originalMap.GetLength(0); x++)
            for (var y = 0; y < originalMap.GetLength(1); y++)
            for (var additionalX = 0; additionalX < sizeMultiplier; additionalX++)
            for (var additionalY = 0; additionalY < sizeMultiplier; additionalY++)
            {
                newMap[x + additionalX * originalMap.GetLength(0), y + additionalY * originalMap.GetLength(1)]
                    = (originalMap[x, y] + additionalX + additionalY - 1) % 9 + 1;
            }

            return newMap;
        }

        private static int[,] readDataFromFile(string fileName)
        {
            var inputFile = new FileInfo(fileName);
            var result = new List<int[]>();
            using var fileStream = inputFile.OpenRead();
            using var fileReader = new StreamReader(fileStream);
            while (!fileReader.EndOfStream)
            {
                result.Add(fileReader.ReadLine()
                    .ToCharArray()
                    .Select(digit => int.Parse(digit.ToString()))
                    .ToArray());
            }

            var map = new int[result.Max(line => line.Length), result.Count];
            for (var x = 0; x < map.GetLength(0); x++)
            for (var y = 0; y < map.GetLength(1); y++)
            {
                map[x, y] = result[y].Length > x ? result[y][x] : -1;
            }

            return map;
        }
    }
}
