using System;
using System.Collections.Generic;
using System.Drawing;

public static class ColorPicker
{
    private static Random _random = new Random();
    private static List<Color> _colors = new List<Color>
    {
        Color.Turquoise,
        Color.RoyalBlue,
        Color.PaleGreen,
        Color.Gold,
        Color.SandyBrown,
        Color.Orchid,
        Color.LightCoral,
        Color.MediumPurple
    };

    public static List<Color> Colors => _colors;

    public static Color GetRandomColor()
    {
        var index = _random.Next(_colors.Count);

        return _colors[index];
    }
}
