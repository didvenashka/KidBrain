using System.Collections.Generic;

public class Match
{
    public int MainPictureId { get; set; }
    public IEnumerable<int> VariantsIds { get; set; }
    public int DurationOfAllVariantsInSeconds { get; set; }
    public int Reward { get; set; }
}
