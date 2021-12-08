using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC04
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
            foreach (var number in input.Numbers)
            {
                foreach (var board in input.Boards)
                {
                    board.MarkNumber(number);
                    if (board.IsAnythingFinished())
                    {
                        var unmarkedSum = board.GetUnmarkedNumbers().Sum();
                        Console.WriteLine(board.ToString());
                        Console.WriteLine($"Number      : {number}");
                        Console.WriteLine($"Unmarked sum: {unmarkedSum}");
                        Console.WriteLine($"{unmarkedSum} * {number} = {unmarkedSum * number}");

                        Console.WriteLine();
                        return;
                    }
                }
            }
        }

        private static void task2(string filename)
        {
            Console.WriteLine("Task 2:");
            Console.WriteLine("=======");

            var input = readDataFromFile(filename);
            foreach (var number in input.Numbers)
            {
                for (var index = input.Boards.Count - 1; index >= 0; index--)
                {
                    var board = input.Boards[index];
                    board.MarkNumber(number);
                    if (board.IsAnythingFinished())
                    {
                        if (input.Boards.Count > 1)
                        {
                            input.Boards.RemoveAt(index);
                        }
                        else
                        {
                            var unmarkedSum = board.GetUnmarkedNumbers().Sum();
                            Console.WriteLine(board.ToString());
                            Console.WriteLine($"Number      : {number}");
                            Console.WriteLine($"Unmarked sum: {unmarkedSum}");
                            Console.WriteLine($"{unmarkedSum} * {number} = {unmarkedSum * number}");

                            Console.WriteLine();
                            return;
                        }
                    }
                }
            }

            Console.WriteLine();
        }

        private static Input readDataFromFile(string fileName)
        {
            var inputFile = new FileInfo(fileName);
            using var fileStream = inputFile.OpenRead();
            using var fileReader = new StreamReader(fileStream);

            var boards = new List<Board>();
            var numbers = fileReader.ReadLine()?
                .Split(',')
                .Select(number => int.Parse(number))
                .ToList();

            while (!fileReader.EndOfStream)
            {
                fileReader.ReadLine();
                var nextBoard = fileReader.ReadLine(); // 1
                nextBoard += " " + fileReader.ReadLine(); // 2
                nextBoard += " " + fileReader.ReadLine(); // 3
                nextBoard += " " + fileReader.ReadLine(); // 4
                nextBoard += " " + fileReader.ReadLine(); // 5

                boards.Add(new Board(nextBoard));
            }

            return new Input
            {
                Numbers = numbers,
                Boards = boards,
            };
        }
    }
}
