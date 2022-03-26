using System;

public class SubtractOperation : IOperation
{
    private readonly Random _random = new Random();

    public int Operate(int firstNumber, int secondNumber)
    {
        return firstNumber - secondNumber;
    }

    public (int, int) GenerateNumbers(Difficulty difficulty)
    {
        var (leftBound, rightBound) = difficulty.SumBounds;

        var firstNumber = _random.Next(leftBound + 1, rightBound);
        var secondNumber = _random.Next(leftBound, firstNumber);

        return (firstNumber, secondNumber);
    }
}
