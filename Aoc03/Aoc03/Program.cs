using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc03
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

            int[] valueCount = null;
            int values = 0;

            foreach (var entry in readEntriesFromFile(filename))
            {
                if (valueCount == null)
                {
                    valueCount = new int[entry.Length];
                }
                else
                {
                    if (entry.Length != valueCount.Length)
                    {
                        throw new ArgumentException("Entry lengths don't match!");
                    }
                }
                values++;
                for (var index = 0; index < entry.Length; index++)
                {
                    if (entry[index])
                    {
                        valueCount[index]++;
                    }
                }
            }

            var gammaString = string.Empty;
            var epsilonString = string.Empty;

            foreach (var value in valueCount)
            {
                gammaString += value * 2 > values ? "1" : "0";
                epsilonString += value * 2 > values ? "0" : "1";
            }

            var gammaValue = Convert.ToInt32(gammaString, 2);
            var epsilonValue = Convert.ToInt32(epsilonString, 2);

            Console.WriteLine($"Gamma  : {gammaString} / {gammaValue}");
            Console.WriteLine($"Epsilon: {epsilonString} / {epsilonValue}");
            Console.WriteLine($"{gammaValue} * {epsilonValue} = {gammaValue * epsilonValue}");

            Console.WriteLine();
        }

        private static void task2(string filename)
        {
            Console.WriteLine("Task 2:");
            Console.WriteLine("=======");

            var valuesOxygen = readEntriesFromFile(filename).ToList();
            var valuesCo2 = valuesOxygen;

            var oxygenEntry = getOxygenValue(valuesOxygen);
            var co2Entry = getCo2Value(valuesCo2);

            var oxygenString = getString(oxygenEntry);
            var co2String = getString(co2Entry);

            var oxygenValue = Convert.ToInt32(oxygenString, 2);
            var co2Value = Convert.ToInt32(co2String, 2);

            Console.WriteLine($"oxygen: {oxygenString} / {oxygenValue}");
            Console.WriteLine($"co2   : {co2String} / {co2Value}");
            Console.WriteLine($"{oxygenValue} * {co2Value} = {oxygenValue * co2Value}");

            Console.WriteLine();
        }

        private static string getString(bool[] value)
        {
            var result = string.Empty;
            foreach (var entry in value)
            {
                result += entry ? "1" : "0";
            }

            return result;
        }

        private static bool[] getOxygenValue(List<bool[]> valuesOxygen)
        {
            for (var index = 0; index < valuesOxygen.First().Length; index++)
            {
                if (valuesOxygen.Sum(entry => entry[index] ? 1 : 0) * 2 >= valuesOxygen.Count)
                {
                    valuesOxygen = valuesOxygen.Where(entry => entry[index]).ToList();
                }
                else
                {
                    valuesOxygen = valuesOxygen.Where(entry => !entry[index]).ToList();
                }

                if (valuesOxygen.Count == 1)
                {
                    return valuesOxygen.Single();
                }
            }

            throw new ArgumentException("No single value found!");
        }

        private static bool[] getCo2Value(List<bool[]> valuesCo2)
        {
            for (var index = 0; index < valuesCo2.First().Length; index++)
            {
                if (valuesCo2.Sum(entry => entry[index] ? 1 : 0) * 2 < valuesCo2.Count)
                {
                    valuesCo2 = valuesCo2.Where(entry => entry[index]).ToList();
                }
                else
                {
                    valuesCo2 = valuesCo2.Where(entry => !entry[index]).ToList();
                }

                if (valuesCo2.Count == 1)
                {
                    return valuesCo2.Single();
                }
            }

            throw new ArgumentException("No single value found!");
        }

        private static IEnumerable<bool[]> readEntriesFromFile(string fileName)
        {
            var regex = new Regex(@"^([01]+)$");
            var inputFile = new FileInfo(fileName);
            using var fileStream = inputFile.OpenRead();
            using var fileReader = new StreamReader(fileStream);
            while (!fileReader.EndOfStream)
            {
                var line = regex.Match(fileReader.ReadLine());
                if (line.Success)
                {
                    List<bool> values = new List<bool>();
                    foreach (var value in line.Value.ToCharArray())
                    {
                        values.Add('1'.Equals(value));
                    }

                    yield return values.ToArray();
                }
            }
        }
    }
}
