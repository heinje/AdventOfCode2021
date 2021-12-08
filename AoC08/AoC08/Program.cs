using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC08
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

            var numberSearchedDigits = readDataFromFile(filename)
                .SelectMany(entry => entry.NumberDigits)
                .Count(number => number.Length == 2 | number.Length == 3 | number.Length == 4 | number.Length == 7);
            Console.WriteLine($"{numberSearchedDigits} digits");
            Console.WriteLine();
        }

        private static void task2(string filename)
        {
            Console.WriteLine("Task 2:");
            Console.WriteLine("=======");

            var numberSum = readDataFromFile(filename)
                .Sum(entry => decodeNumber(entry));
            Console.WriteLine($"Sum: {numberSum}");
            Console.WriteLine();
        }

        private static IEnumerable<Entry> readDataFromFile(string fileName)
        {
            var inputFormat = new Regex(@"^([a-g ]+) \| ([a-g ]+)$");
            var inputFile = new FileInfo(fileName);
            using var fileStream = inputFile.OpenRead();
            using var fileReader = new StreamReader(fileStream);
            while (!fileReader.EndOfStream)
            {
                var match = inputFormat.Match(fileReader.ReadLine());
                if (match.Success)
                {
                    yield return new Entry
                    {
                        AllDigits = match.Groups[1].Value.Split(' ', StringSplitOptions.RemoveEmptyEntries),
                        NumberDigits = match.Groups[2].Value.Split(' ', StringSplitOptions.RemoveEmptyEntries),
                    };
                }
            }
        }

        private static int decodeNumber(Entry entry)
        {
            var encoded = new Dictionary<int, string>();
            encoded[1] = entry.AllDigits.First(number => number.Length == 2);
            encoded[4] = entry.AllDigits.First(number => number.Length == 4);
            encoded[7] = entry.AllDigits.First(number => number.Length == 3);
            encoded[8] = entry.AllDigits.First(number => number.Length == 7);

            var remaining = entry.AllDigits.Except(encoded.Values).ToList();
            
            encoded[9] = remaining.First(number => containsAll(number, encoded[4]));
            remaining.Remove(encoded[9]);

            encoded[0] = remaining.First(number => number.Length == 6 && containsAll(number, encoded[7]));
            remaining.Remove(encoded[0]);

            encoded[6] = remaining.First(number => number.Length == 6);
            remaining.Remove(encoded[6]);

            encoded[3] = remaining.First(number => containsAll(number, encoded[7]));
            remaining.Remove(encoded[3]);

            encoded[5] = remaining.First(number => countMatches(number, encoded[4]) == 3);
            remaining.Remove(encoded[5]);

            encoded[2] = remaining.Single();

            var decoded = encoded.ToDictionary(number => sort(number.Value).ToString(), number => number.Key);
            return decoded[sort(entry.NumberDigits[0])] * 1000 +
                   decoded[sort(entry.NumberDigits[1])] * 100 +
                   decoded[sort(entry.NumberDigits[2])] * 10 +
                   decoded[sort(entry.NumberDigits[3])];
        }

        private static bool containsAll(string toCheck, IEnumerable<char> characters)
        {
            foreach (var character in characters)
            {
                if (!toCheck.Contains(character))
                {
                    return false;
                }
            }

            return true;
        }

        private static int countMatches(string toCheck, IEnumerable<char> characters)
        {
            var matches = 0;
            foreach (var character in characters)
            {
                if (toCheck.Contains(character))
                {
                    matches++;
                }
            }

            return matches;
        }

        private static string sort(string characters)
        {
            return new string(characters.OrderBy(character => character).ToArray());
        }
    }
}
