public abstract class Difficulty
{
    public abstract int Points { get; }
    public abstract (int, int) SumBounds { get; }
    public abstract (int, int) MultiplyBounds { get; }
    public abstract int MultiplyFactor { get; }
}
