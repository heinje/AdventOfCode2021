using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Drawing;
using System.Text.RegularExpressions;

namespace AoC05
{
    public class Entry
    {
        private static readonly Regex _inputFormat = new Regex(@"^(\d+),(\d+) -> (\d+),(\d+)$");

        private readonly Point _start;
        private readonly Point _end;
        
        public Entry(string line)
        {
            var match = _inputFormat.Match(line);
            if (!match.Success)
            {
                throw new ArgumentException("Line can't be read!");
            }

            _start = new Point(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
            _end = new Point(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));
        }

        public bool IsHorizontal => _start.Y == _end.Y;
        public bool IsVertical => _start.X == _end.X;
        public bool IsDiagonal => Math.Abs(_start.X - _end.X) == Math.Abs(_start.Y - _end.Y);

        public IEnumerable<Point> getPointsOnLine()
        {
            if (IsHorizontal)
            {
                var xStart = Math.Min(_start.X, _end.X);
                var xEnd = Math.Max(_start.X, _end.X);
                for (var x = xStart; x <= xEnd; x++)
                {
                    yield return new Point(x, _start.Y);
                }
                yield break;
            }
            
            if (IsVertical)
            {
                var yStart = Math.Min(_start.Y, _end.Y);
                var yEnd = Math.Max(_start.Y, _end.Y);
                for (var y = yStart; y <= yEnd; y++)
                {
                    yield return new Point(_start.X, y);
                }
                yield break;
            }
            
            if (IsDiagonal)
            {
                var point = _start;
                while (point != _end)
                {
                    yield return point;
                    point = new Point
                    {
                        X = point.X + (_end.X > _start.X ? 1 : -1),
                        Y = point.Y + (_end.Y > _start.Y ? 1 : -1)
                    };
                }
                yield return _end;
                yield break;
            }

            throw new DataMisalignedException($"Invalid Data: {_start} -> {_end}");
        }
    }
}