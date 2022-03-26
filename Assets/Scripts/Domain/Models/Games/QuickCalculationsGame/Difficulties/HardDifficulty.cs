public class HardDifficulty : Difficulty
{
    public override int Points => 9;
    public override (int, int) SumBounds => (100, 1000);
    public override (int, int) MultiplyBounds => (10, 50);
    public override int MultiplyFactor => 1;
}
