using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC14
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

            task(filename, 10);

            Console.WriteLine();
        }

        private static void task2(string filename)
        {
            Console.WriteLine("Task 2:");
            Console.WriteLine("=======");

            task(filename, 40);

            Console.WriteLine();
        }

        private static void task(string filename, int steps)
        {
            var input = readDataFromFile(filename);
            var sequence = input.StartingSequence;
            var characterPairs = new Dictionary<string, long>();
            for (var index = 0; index < sequence.Length - 1; index++)
            {
                addCharacterPairCount(characterPairs, sequence.Substring(index, 2), 1);
            }

            for (var step = 0; step < steps; step++)
            {
                var nextCharacterPairs = new Dictionary<string, long>();
                foreach (var characterPair in characterPairs.Keys)
                {
                    if (!input.Substitutions.ContainsKey(characterPair))
                    {
                        addCharacterPairCount(nextCharacterPairs, characterPair, characterPairs[characterPair]);
                    }
                    else
                    {
                        var newCharacterPairA = characterPair.Substring(0, 1) + input.Substitutions[characterPair];
                        var newCharacterPairB = input.Substitutions[characterPair] + characterPair.Substring(1, 1);
                        addCharacterPairCount(nextCharacterPairs, newCharacterPairA, characterPairs[characterPair]);
                        addCharacterPairCount(nextCharacterPairs, newCharacterPairB, characterPairs[characterPair]);
                    }
                }

                characterPairs = nextCharacterPairs;
            }

            var characterCount = new Dictionary<string, long>();
            foreach (var characterPair in characterPairs)
            {
                addCharacterPairCount(characterCount, characterPair.Key.Substring(0, 1), characterPair.Value);
            }

            addCharacterPairCount(characterCount, sequence.Substring(sequence.Length - 1), 1);

            var characters = characterCount
                .OrderBy(entry => entry.Value)
                .ToList();

            foreach (var entry in characters)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }

            var minimum = characters.First().Value;
            var maximum = characters.Last().Value;

            Console.WriteLine($"{maximum} - {minimum} = {maximum - minimum}");
        }

        private static void addCharacterPairCount(Dictionary<string, long> characterPairs, string key, long value)
        {
            if (!characterPairs.ContainsKey(key))
            {
                characterPairs[key] = value;
            }
            else
            {
                characterPairs[key] += value;
            }
        }

        private static Input readDataFromFile(string fileName)
        {
            var inputFile = new FileInfo(fileName);
            var substitutionPattern = new Regex(@"^([A-Z]{2}) -\> ([A-Z])$");
            using var fileStream = inputFile.OpenRead();
            using var fileReader = new StreamReader(fileStream);
            var input = new Input
            {
                StartingSequence = fileReader.ReadLine(),
                Substitutions = new Dictionary<string, string>(),
            };
            while (!fileReader.EndOfStream)
            {
                var match = substitutionPattern.Match(fileReader.ReadLine());
                if (match.Success)
                {
                    input.Substitutions[match.Groups[1].Value] = match.Groups[2].Value;
                }
            }

            return input;
        }
    }
}
