using System.Collections.Generic;
using System.Drawing;

namespace AoC15
{
    public class Path
    {
        public List<Point> Points { get; } = new List<Point> {new Point(0, 0)};
        public int Length { get; private set; } = 0;
    }
}