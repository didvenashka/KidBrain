using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class ScoreRepository : IScoreRepository
{
    private readonly string _pathBase = Application.persistentDataPath;
    private readonly string _fileName = "statistic.json";

    public void AddPoints(int points, Type gameType, DateTime dateTime)
    {
        var path = Path.Combine(_pathBase, _fileName);

        var statistic = GetStatistic();

        if (!statistic.GameStatistics.ContainsKey(gameType.Name))
        {
            statistic.GameStatistics[gameType.Name] = new GameStatistic();
        }

        var gameStatistic = statistic.GameStatistics[gameType.Name];

        if (!gameStatistic.DailyStatistics.ContainsKey(dateTime.ToString()))
        {
            gameStatistic.DailyStatistics[dateTime.ToString()] = new DailyStatistic();
        }

        var dailyStatistic = gameStatistic.DailyStatistics[dateTime.ToString()];

        dailyStatistic.Points += points;
        dailyStatistic.NumberOfGames++;
        dailyStatistic.AveragePointsPerGame = dailyStatistic.Points / dailyStatistic.NumberOfGames;

        var json = JsonConvert.SerializeObject(statistic);
        File.WriteAllText(path, json);
    }

    public Statistic GetStatistic()
    {
        var path = Path.Combine(_pathBase, _fileName);

        var statistic = new Statistic();
        if (!File.Exists(path))
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(statistic));
        }
        else
        {
            statistic = JsonConvert.DeserializeObject<Statistic>(File.ReadAllText(path));
        }
        return statistic;
    }
}
