public class MediumDifficulty : Difficulty
{
    public override int Points => 8;
    public override (int, int) SumBounds => (10, 100);
    public override (int, int) MultiplyBounds => (2, 10);
    public override int MultiplyFactor => 5;
}
