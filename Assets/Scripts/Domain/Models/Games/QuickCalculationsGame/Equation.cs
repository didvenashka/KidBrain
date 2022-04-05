using System.Collections.Generic;

public class Equation
{
    public int FirstNumber { get; set; }
    public int SecondNumber { get; set; }
    public int Answer { get; set; }
    public Position HiddenPosition { get; set; }
    public IOperation Operation { get; set; }
    public IEnumerable<int> Variants { get; set; }
    public int Reward { get; set; }

    public string StringRepresentation()
    {
        string[] components = new string[3]
        {
            FirstNumber.ToString(),
            SecondNumber.ToString(),
            Answer.ToString(),
        };
        int hidden = (int)HiddenPosition;
        components[hidden] = "?";
        return $"{components[0]} {Operation.Symbol()} {components[1]} = {components[2]}";
    }
}
