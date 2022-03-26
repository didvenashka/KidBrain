using System.Collections.Generic;

public class Equation
{
    public int FirstNumber { get; set; }
    public int SecondNumber { get; set; }
    public int Answer { get; set; }
    public Position HiddenPosition { get; set; }
    public IOperation Operation { get; set; }
    public IEnumerable<int> Variants { get; set; }
}
