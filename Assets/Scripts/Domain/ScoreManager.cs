using System;

public class ScoreManager : IScoreManager
{
    private readonly IScoreRepository _scoreRepository;

    public ScoreManager(IScoreRepository scoreRepository)
    {
        _scoreRepository = scoreRepository;
    }

    public void AddPoints(int points, Type gameType)
    {
        _scoreRepository.AddPoints(points, gameType, DateTime.Now.Date);
    }
}
