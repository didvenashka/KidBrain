using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

public class QuickCalculationsGameManager : IQuickCalculationsGameManager
{
    private readonly Random _random = new Random();
    private const int _numberOfVariants = 4;
    private const int _shiftPercent = 20;
    private const int _minShift = 4;
    private readonly int _numberOfPositions = Enum.GetValues(typeof(Position)).Length;

    public QuickCalculationsGame CreateNewGame()
    {
        var equations = new List<Equation>();

        var operations = GetOperations();
        var difficulties = GetDifficulties();

        foreach (var difficulty in difficulties)
        {
            operations = operations
                .OrderBy(o => Guid.NewGuid())
                .ToList();

            foreach (var operation in operations)
            {
                var equation = CreateEquation(difficulty, operation);
                equations.Add(equation);
            }
        }

        return new QuickCalculationsGame
        {
            Equations = equations
        };
    }

    private List<Difficulty> GetDifficulties()
    {
        return Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsSubclassOf(typeof(Difficulty)))
            .Select(t => Activator.CreateInstance(t) as Difficulty)
            .OrderBy(d => d.Points)
            .ToList();
    }

    private List<IOperation> GetOperations()
    {
        return Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(IOperation).IsAssignableFrom(t) && !t.IsInterface)
            .Select(t => Activator.CreateInstance(t) as IOperation)
            .ToList();
    }

    private Equation CreateEquation(Difficulty difficulty, IOperation operation)
    {
        var (firstNumber, secondNumber) = operation.GenerateNumbers(difficulty);

        int answer = operation.Operate(firstNumber, secondNumber);

        var hiddenPosition = (Position)_random.Next(_numberOfPositions);

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
            .Take(_numberOfVariants)
            .ToArray();

        if (!variants.Contains(hiddenNumber))
        {
            variants[_random.Next(_numberOfVariants)] = hiddenNumber;
        }

        return new Equation
        {
            FirstNumber = firstNumber,
            SecondNumber = secondNumber,
            Answer = answer,
            HiddenPosition = hiddenPosition,
            Operation = operation,
            Variants = variants,
            Reward = difficulty.Points
        };
    }

    private (int, int) GetBounds(int hiddenNumber)
    {
        var shift = hiddenNumber * _shiftPercent / 100;
        shift = Math.Max(_minShift, shift);
        var (leftBound, rightBound) = (hiddenNumber - shift, hiddenNumber + shift);
        leftBound = Math.Max(1, leftBound);

        return (leftBound, rightBound);
    }
}
