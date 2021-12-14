using System.Collections.Generic;
using System.Drawing;

namespace AoC13
{
    public class Input
    {
        public List<Point> Dots { get; } = new List<Point>();
        public List<FoldingInstruction> FoldingInstructions { get; } = new List<FoldingInstruction>();
    }
}