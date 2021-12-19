namespace AoC16
{
    public abstract class Package
    {
        public short Version { get; set; }
        public EnmPackageType Type { get; set; }

        public abstract int GetVersionSum();
        public abstract long GetValue();
    }
}