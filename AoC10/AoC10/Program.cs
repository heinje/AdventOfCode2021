using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC10
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

            var result = readDataFromFile(filename).Sum(calculateCorruptedScore);
            Console.WriteLine($"Result: {result}");

            Console.WriteLine();
        }

        private static void task2(string filename)
        {
            Console.WriteLine("Task 2:");
            Console.WriteLine("=======");

            var scores = readDataFromFile(filename)
                .Select(calculateIncompleteScore)
                .Where(score => score > 0)
                .OrderBy(score => score)
                .ToList();
            Console.WriteLine($"Result: {scores.Skip((scores.Count - 1) / 2).First()}");

            Console.WriteLine();
        }

        private static IEnumerable<string> readDataFromFile(string fileName)
        {
            var inputFile = new FileInfo(fileName);
            using var fileStream = inputFile.OpenRead();
            using var fileReader = new StreamReader(fileStream);
            while (!fileReader.EndOfStream)
            {
                yield return fileReader.ReadLine();
            }
        }

        private static int calculateCorruptedScore(string line)
        {
            var openBlocks = new Stack<char>();
            foreach (var character in line)
            {
                switch (character)
                {
                    case '(':
                    case '[':
                    case '{':
                    case '<':
                        openBlocks.Push(character);
                        break;
                    case ')':
                        if (openBlocks.Pop() != '(')
                        {
                            return 3;
                        }
                        break;
                    case ']':
                        if (openBlocks.Pop() != '[')
                        {
                            return 57;
                        }
                        break;
                    case '}':
                        if (openBlocks.Pop() != '{')
                        {
                            return 1197;
                        }
                        break;
                    case '>':
                        if (openBlocks.Pop() != '<')
                        {
                            return 25137;
                        }
                        break;
                }
            }

            return 0;
        }

        private static long calculateIncompleteScore(string line)
        {
            var openBlocks = new Stack<char>();
            foreach (var character in line)
            {
                switch (character)
                {
                    case '(':
                    case '[':
                    case '{':
                    case '<':
                        openBlocks.Push(character);
                        break;
                    case ')':
                        if (openBlocks.Pop() != '(')
                        {
                            return 0;
                        }
                        break;
                    case ']':
                        if (openBlocks.Pop() != '[')
                        {
                            return 0;
                        }
                        break;
                    case '}':
                        if (openBlocks.Pop() != '{')
                        {
                            return 0;
                        }
                        break;
                    case '>':
                        if (openBlocks.Pop() != '<')
                        {
                            return 0;
                        }
                        break;
                }
            }

            long score = 0;
            while(openBlocks.Count > 0)
            {
                score *= 5;
                switch (openBlocks.Pop())
                {
                    case '(':
                        score += 1;
                        break;
                    case '[':
                        score += 2;
                        break;
                    case '{':
                        score += 3;
                        break;
                    case '<':
                        score += 4;
                        break;
                }
            }

            return score;
        }
    }
}
