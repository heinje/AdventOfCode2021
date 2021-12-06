using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Aoc02
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
            var depth = 0;
            var horizontalDistance = 0;
            foreach (var entry in readEntriesFromFile(filename))
            {
                switch (entry.Direction)
                {
                    case EnmDirection.forward:
                        horizontalDistance += entry.Distance;
                        break;
                    case EnmDirection.down:
                        depth += entry.Distance;
                        break;
                    case EnmDirection.up:
                        depth -= entry.Distance;
                        break;
                }
            }

            Console.WriteLine($"Horizontal distance: {horizontalDistance}");
            Console.WriteLine($"Depth: {depth}");
            Console.WriteLine($"{horizontalDistance} * {depth} = {horizontalDistance * depth}");
            Console.WriteLine();
        }

        private static void task2(string filename)
        {
            Console.WriteLine("Task 2:");
            Console.WriteLine("=======");
            var depth = 0;
            var horizontalDistance = 0;
            var aim = 0;
            foreach (var entry in readEntriesFromFile(filename))
            {
                switch (entry.Direction)
                {
                    case EnmDirection.forward:
                        horizontalDistance += entry.Distance;
                        depth += aim * entry.Distance;
                        break;
                    case EnmDirection.down:
                        aim += entry.Distance;
                        break;
                    case EnmDirection.up:
                        aim -= entry.Distance;
                        break;
                }
            }

            Console.WriteLine($"Horizontal distance: {horizontalDistance}");
            Console.WriteLine($"Depth: {depth}");
            Console.WriteLine($"{horizontalDistance} * {depth} = {horizontalDistance * depth}");
            Console.WriteLine();
        }

        private static IEnumerable<Entry> readEntriesFromFile(string fileName)
        {
            var regex = new Regex(@"^([A-Za-z]+) (\d+)$");
            var inputFile = new FileInfo(fileName);
            using var fileStream = inputFile.OpenRead();
            using var fileReader = new StreamReader(fileStream);
            while (!fileReader.EndOfStream)
            {
                var line = regex.Match(fileReader.ReadLine());
                if (line.Success 
                    && Enum.TryParse(line.Groups[1].Value, out EnmDirection direction) 
                    && int.TryParse(line.Groups[2].Value, out int distance))
                {
                    yield return new Entry()
                    {
                        Direction = direction,
                        Distance = distance,
                    };
                }
            }
        }
    }
}
