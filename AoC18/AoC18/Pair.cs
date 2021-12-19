namespace AoC18
{
    public class Pair : IEntry
    {
        public IEntry Left { get; set; }
        public IEntry Right { get; set; }

        public bool Reduce(int depth)
        {
            throw new System.NotImplementedException();
        }
    }
}
