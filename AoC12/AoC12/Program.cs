using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AoC12
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

            var connections = readDataFromFile(filename);
            var finishedPaths = getPaths(connections, (path, cave) => path.CanBeAddedEasy(cave));
            Console.WriteLine($"{finishedPaths.Count} paths");

            Console.WriteLine();
        }

        private static void task2(string filename)
        {
            Console.WriteLine("Task 2:");
            Console.WriteLine("=======");

            var connections = readDataFromFile(filename);
            var finishedPaths = getPaths(connections, (path, cave) => path.CanBeAddedComplicated(cave));
            Console.WriteLine($"{finishedPaths.Count} paths");
            
            Console.WriteLine();
        }

        private static List<Path> getPaths(Dictionary<Cave, List<Cave>> connections, Func<Path,Cave,bool> canBeAdded)
        {
            var finishedPaths = new List<Path>();
            var unfinishedPaths = new Stack<Path>();
            unfinishedPaths.Push(new Path(new Cave("start")));
            while (unfinishedPaths.Count > 0)
            {
                var path = unfinishedPaths.Pop();
                foreach (var cave in connections[path.Last()])
                {
                    if (canBeAdded(path,cave))
                    {
                        var newPath = path.GetNewPath(cave);
                        if (newPath.IsFinished())
                        {
                            finishedPaths.Add(newPath);
                        }
                        else
                        {
                            unfinishedPaths.Push(newPath);
                        }
                    }
                }
            }

            return finishedPaths;
        }

        private static Dictionary<Cave,List<Cave>> readDataFromFile(string fileName)
        {
            var pattern = new Regex("^([a-zA-Z]+)-([a-zA-Z]+)$");
            var inputFile = new FileInfo(fileName);
            var connections = new Dictionary<Cave, List<Cave>>();
            using var fileStream = inputFile.OpenRead();
            using var fileReader = new StreamReader(fileStream);
            while (!fileReader.EndOfStream)
            {
                var match = pattern.Match(fileReader.ReadLine());
                if (match.Success)
                {
                    var caveFrom = new Cave(match.Groups[1].Value);
                    var caveTo = new Cave(match.Groups[2].Value);
                    addConnection(connections, caveFrom, caveTo);
                    addConnection(connections, caveTo, caveFrom);
                }
            }

            return connections;
        }

        private static void addConnection(Dictionary<Cave, List<Cave>> connections, Cave caveA, Cave caveB)
        {
            if (!connections.ContainsKey(caveA))
            {
                connections[caveA] = new List<Cave> {caveB};
            }
            else
            {
                connections[caveA].Add(caveB);
            }
        }
    }
}
