namespace AoC16
{
    public class LiteralPackage : Package
    {
        public long Value { get; set; }

        public override int GetVersionSum()
        {
            return Version;
        }

        public override long GetValue()
        {
            return Value;
        }
    }
}