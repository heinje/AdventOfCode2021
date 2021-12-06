using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc04
{
    public class Board
    {
        private int[] _numbers;
        private bool[] _marked;

        public Board(string lines)
        {
            var numbers = lines.Split(new[] {' ', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            if (numbers.Length != 25)
            {
                throw new ArgumentException("Board numbers can't be read!");
            }

            _numbers = numbers.Select(int.Parse).ToArray();
            _marked = new bool[_numbers.Length];
        }

        public void MarkNumber(int number)
        {
            for (var index = 0; index < _numbers.Length; index++)
            {
                if (_numbers[index] == number)
                {
                    _marked[index] = true;
                }
            }
        }

        public IEnumerable<int> GetUnmarkedNumbers()
        {
            for (var index = 0; index < _marked.Length; index++)
            {
                if (!_marked[index])
                {
                    yield return _numbers[index];
                }
            }
        }

        public bool IsAnythingFinished()
        {
            return isAnyLineFinished() || isAnyColumnFinished();
        }

        private bool isAnyLineFinished()
        {
            for (var index = 0; index < 5; index++)
            {
                if (isLineFinished(index))
                {
                    return true;
                }
            }

            return false;
        }

        private bool isLineFinished(int lineIndex)
        {
            for (var index = lineIndex*5; index < (lineIndex+1) * 5; index++)
            {
                if (!_marked[index])
                {
                    return false;
                }
            }

            return true;
        }

        private bool isAnyColumnFinished()
        {
            for (var index = 0; index < 5; index++)
            {
                if (isColumnFinished(index))
                {
                    return true;
                }
            }

            return false;
        }

        private bool isColumnFinished(int columnIndex)
        {
            for (var index = columnIndex; index < _marked.Length; index += 5)
            {
                if (!_marked[index])
                {
                    return false;
                }
            }

            return true;
        }

        public string ToString()
        {
            StringBuilder result = new StringBuilder();
            for (var index = 0; index < _numbers.Length; index++)
            {
                if (index > 0 && index % 5 == 0)
                {
                    result.AppendLine();
                }

                result.Append(printNumber(index));
            }

            return result.ToString();
        }

        private string printNumber(int index)
        {
            return _marked[index]
                ? $"*{_numbers[index]:D2}* "
                : $" {_numbers[index]:D2}  ";
        }
    }
}