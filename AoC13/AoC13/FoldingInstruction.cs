namespace AoC13
{
    public class FoldingInstruction
    {
        public EnmAxis Axis { get; }
        public int Value { get; }

        public FoldingInstruction(EnmAxis axis, int value)
        {
            Axis = axis;
            Value = value;
        }
    }
}