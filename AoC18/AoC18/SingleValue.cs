namespace AoC18
{
    public class SingleValue : IEntry
    {
        public int Value { get; set; }

        public bool Reduce(int depth)
        {
            return false;
        }
    }
}
