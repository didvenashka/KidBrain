using System;

public class MultiplyOperation : IOperation
{
    private readonly Random _random = new Random();

    public int Operate(int firstNumber, int secondNumber)
    {
        return firstNumber * secondNumber;
    }

    public (int, int) GenerateNumbers(Difficulty difficulty)
    {
        var (leftBound, rightBound) = difficulty.MultiplyBounds;
        var factor = difficulty.MultiplyFactor;

        var firstNumber = _random.Next(leftBound, rightBound);
        var secondNumber = _random.Next(leftBound * factor, rightBound * factor);

        return (firstNumber, secondNumber);
    }
}
