using System.Collections.Generic;

public class GameStatistic
{
    public GameStatistic()
    {
        DailyStatistics = new Dictionary<string, DailyStatistic>();
    }

    public Dictionary<string, DailyStatistic> DailyStatistics { get; set; }
}
