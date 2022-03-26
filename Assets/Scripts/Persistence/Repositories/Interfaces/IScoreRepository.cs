using System;

public interface IScoreRepository
{
    void AddPoints(int points, Type gameType, DateTime dateTime);
}
