using System;
using System.Drawing;

namespace AoC17
{
    public class Input
    {
        public Point Minimum { get; }
        public Point Maximum { get; }

        public Input(int x1, int x2, int y1, int y2)
        {
            Minimum = new Point(Math.Min(x1, x2), Math.Min(y1, y2));
            Maximum = new Point(Math.Max(x1, x2), Math.Max(y1, y2));
        }

        public bool IsWithin(Point point)
        {
            return point.X >= Minimum.X
                && point.X <= Maximum.X
                && point.Y >= Minimum.Y
                && point.Y <= Maximum.Y;
        }

        public bool HasMissed(Point point)
        {
            return point.X > Maximum.X
                || point.Y < Minimum.Y;                
        }
    }
}
