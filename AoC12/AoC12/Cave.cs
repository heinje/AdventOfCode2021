namespace AoC12
{
    public class Cave
    {
        public string Name { get; }
        public bool IsBig { get; }

        public Cave(string name)
        {
            Name = name;
            IsBig = Name == Name.ToUpper();
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return Name.Equals((obj as Cave)?.Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
