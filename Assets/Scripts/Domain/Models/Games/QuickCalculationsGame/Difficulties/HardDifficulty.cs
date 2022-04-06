﻿public class HardDifficulty : Difficulty
{
    public override int Points => 13;
    public override (int, int) SumBounds => (100, 1000);
    public override (int, int) MultiplyBounds => (10, 20);
    public override int ShiftFactor => 0;
}
