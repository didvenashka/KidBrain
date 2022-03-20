using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

public class QuickCalculationsGameManager : IQuickCalculationsGameManager
{
    private static readonly Random _random = new Random();

    public QuickCalculationsGame CreateNewGame()
    {
        var equations = new List<Equation>();

        foreach (var difficulty in (Difficulty[])Enum.GetValues(typeof(Difficulty)))
        {
            for (int i = 0; i < 4; i++)
            {
                var operation = (Operation)_random.Next(2 * (i / 2), 2 * (i / 2) + 2);
                equations.Add(CreateEquation(difficulty, operation));
            }
        }

        return new QuickCalculationsGame
        {
            Equations = equations
        };
    }

    private static Equation CreateEquation(Difficulty difficulty, Operation operation)
    {
        var (firstNumber, secondNumber) = GenerateNumbers(difficulty, operation);

        int answer = GetAnswer(operation, firstNumber, secondNumber);

        var hiddenPosition = (Position)_random.Next(3);

        var hiddenNumber = hiddenPosition switch
        {
            Position.First => firstNumber,
            Position.Second => secondNumber,
            Position.Answer => answer,
            _ => throw new InvalidEnumArgumentException()
        };

        var (leftBound, rightBound) = GetBounds(hiddenNumber);

        var variants = Enumerable
            .Range(leftBound, rightBound - leftBound)
            .OrderBy(x => Guid.NewGuid())
            .Take(4)
            .ToArray();

        if (!variants.Contains(hiddenNumber))
        {
            variants[_random.Next(4)] = hiddenNumber;
        }

        return new Equation
        {
            FirstNumber = firstNumber,
            SecondNumber = secondNumber,
            Answer = answer,
            HiddenPosition = hiddenPosition,
            Operation = operation,
            Variants = variants
        };
    }

    private static int GetAnswer(Operation operation, int firstNumber, int secondNumber)
    {
        return operation switch
        {
            Operation.Add => firstNumber + secondNumber,
            Operation.Subtract => firstNumber - secondNumber,
            Operation.Multiply => firstNumber * secondNumber,
            Operation.Divide => firstNumber / secondNumber,
            _ => throw new InvalidEnumArgumentException()
        };
    }

    private static (int, int) GenerateNumbers(Difficulty difficulty, Operation operation)
    {
        return difficulty switch
        {
            Difficulty.Easy => GenerateNumbersForEasy(operation),
            Difficulty.Medium => GenerateNumbersForMedium(operation),
            Difficulty.Hard => GenerateNumbersForHard(operation),
            _ => throw new InvalidEnumArgumentException()
        };
    }

    private static (int, int) GenerateNumbersForEasy(Operation operation)
    {
        var firstNumber = 0;
        var secondNumber = 0;

        if (operation == Operation.Add)
        {
            firstNumber = _random.Next(1, 10);
            secondNumber = _random.Next(1, 10);
        }

        if (operation == Operation.Subtract)
        {
            firstNumber = _random.Next(2, 10);
            secondNumber = _random.Next(1, firstNumber);
        }

        if (operation == Operation.Multiply)
        {
            firstNumber = _random.Next(2, 10);
            secondNumber = _random.Next(2, 10);
        }

        if (operation == Operation.Divide)
        {
            secondNumber = _random.Next(2, 10);
            firstNumber = secondNumber * _random.Next(2, 10);
        }

        return (firstNumber, secondNumber);
    }

    private static (int, int) GenerateNumbersForMedium(Operation operation)
    {
        var firstNumber = 0;
        var secondNumber = 0;

        if (operation == Operation.Add)
        {
            firstNumber = _random.Next(10, 100);
            secondNumber = _random.Next(10, 100);
        }

        if (operation == Operation.Subtract)
        {
            firstNumber = _random.Next(11, 100);
            secondNumber = _random.Next(10, firstNumber);
        }

        if (operation == Operation.Multiply)
        {
            firstNumber = _random.Next(2, 10);
            secondNumber = _random.Next(10, 50);
        }

        if (operation == Operation.Divide)
        {
            secondNumber = _random.Next(10, 50);
            firstNumber = secondNumber * _random.Next(2, 10);
        }

        return (firstNumber, secondNumber);
    }

    private static (int, int) GenerateNumbersForHard(Operation operation)
    {
        var firstNumber = 0;
        var secondNumber = 0;

        if (operation == Operation.Add)
        {
            firstNumber = _random.Next(100, 1000);
            secondNumber = _random.Next(100, 1000);
        }

        if (operation == Operation.Subtract)
        {
            firstNumber = _random.Next(101, 1000);
            secondNumber = _random.Next(100, firstNumber);
        }

        if (operation == Operation.Multiply)
        {
            firstNumber = _random.Next(10, 50);
            secondNumber = _random.Next(10, 50);
        }

        if (operation == Operation.Divide)
        {
            secondNumber = _random.Next(10, 50);
            firstNumber = secondNumber * _random.Next(10, 50);
        }

        return (firstNumber, secondNumber);
    }

    private static (int, int) GetBounds(int hiddenNumber)
    {
        var leftBound = 1;
        var rightBound = 0;

        if (hiddenNumber < 10)
        {
            rightBound = 10;
        }
        else if (hiddenNumber < 30)
        {
            leftBound = 10;
            rightBound = 50;
        }
        else if (hiddenNumber < 100)
        {
            leftBound = hiddenNumber - 20;
            rightBound = hiddenNumber + 20;
        }
        else if (hiddenNumber < 300)
        {
            leftBound = 100;
            rightBound = 400;
        }
        else
        {
            leftBound = hiddenNumber - 200;
            rightBound = hiddenNumber + 200;
        }

        return (leftBound, rightBound);
    }
}
