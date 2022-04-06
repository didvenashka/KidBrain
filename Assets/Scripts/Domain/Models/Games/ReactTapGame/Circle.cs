using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

public class Circle
{
    public float Radius { get; set; }
    public float Speed { get; set; }
    public int DurationInSeconds { get; set; }
    public Color Color { get; set; }
    public IEnumerable<Vector2> Path { get; set; }
    public int Reward { get; set; }
}
