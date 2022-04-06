public class EasyDifficulty : Difficulty
{
    public override int Points => 4;
    public override (int, int) SumBounds => (1, 10);
    public override (int, int) MultiplyBounds => (2, 10);
    public override int ShiftFactor => 0;
}
