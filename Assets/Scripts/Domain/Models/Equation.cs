public class Equation
{
    public int FirstNumber { get; set; }
    public int SecondNumber { get; set; }
    public int Answer { get; set; }
    public Position HiddenPosition { get; set; }
    public Operation Operation { get; set; }
    public int[] Variants { get; set; } = new int[4];
}
