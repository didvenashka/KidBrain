using System.Collections.Generic;

public class Statistic
{
    public Statistic()
    {
        GameStatistics = new Dictionary<string, GameStatistic>();
    }

    public Dictionary<string, GameStatistic> GameStatistics { get; set; }
}
