using System;
using System.Collections.Generic;
using System.Linq;

public class MemorySquaresGameManager : IMemorySquaresGameManager
{
    private readonly Random _random = new Random();
    private const int _numberOfUnits = 4;
    private const int _numberOfSequences = 12;
    private const int _durationInSeconds = 8;
    private const int _minLength = 4;

    public MemorySquaresGame CreateNewGame()
    {
        var sequences = CreateSequences(_numberOfSequences);

        return new MemorySquaresGame
        {
            Sequences = sequences
        };
    }

    private IEnumerable<Sequence> CreateSequences(int numberOfSequences)
    {
        var sequences = new List<Sequence>();

        for (int i = 0; i < numberOfSequences; i++)
        {
            var sequence = CreateSequence(_minLength + i / 2);
            sequences.Add(sequence);
        }

        return sequences;
    }

    private Sequence CreateSequence(int length)
    {
        var sequence = new List<int>();

        for (int i = 0; i < length; i++)
        {
            int randomIndex = _random.Next(_numberOfUnits);
            if (sequence.Any() && randomIndex == sequence.Last())
            {
                randomIndex = _random.Next(_numberOfUnits);
            }
            sequence.Add(randomIndex);
        }

        var reward = length > _minLength ? length + 2 : length + 1;

        return new Sequence
        {
            Order = sequence,
            DurationInSeconds = _durationInSeconds,
            Reward = reward
        };
    }
}
