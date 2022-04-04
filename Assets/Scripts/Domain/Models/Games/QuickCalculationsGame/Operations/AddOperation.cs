using System;

public class AddOperation : IOperation
{
    private readonly Random _random = new Random();

    public int Operate(int firstNumber, int secondNumber)
    {
        return firstNumber + secondNumber;
    }

    public (int, int) GenerateNumbers(Difficulty difficulty)
    {
        var (leftBound, rightBound) = difficulty.SumBounds;

        var firstNumber = _random.Next(leftBound, rightBound);
        var secondNumber = _random.Next(leftBound, rightBound);

        return (firstNumber, secondNumber);
    }

    public string Symbol() => "+";
}
