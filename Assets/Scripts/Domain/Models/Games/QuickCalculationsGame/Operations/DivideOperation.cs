using System;

public class DivideOperation : IOperation
{
    private readonly Random _random = new Random();

    public int Operate(int firstNumber, int secondNumber)
    {
        return firstNumber / secondNumber;
    }

    public (int, int) GenerateNumbers(Difficulty difficulty)
    {
        var (leftBound, rightBound) = difficulty.MultiplyBounds;
        var factor = difficulty.ShiftFactor;

        var secondNumber = _random.Next(leftBound + factor, rightBound + factor);
        var firstNumber = secondNumber * _random.Next(leftBound, rightBound);

        return (firstNumber, secondNumber);
    }

    public string Symbol() => "÷";
}
