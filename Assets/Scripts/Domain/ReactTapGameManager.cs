using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

public class ReactTapGameManager : IReactTapGameManager
{
    private static readonly Random _random = new Random();
    private const int _gameDurationInSeconds = 30;
    private const int _circleDurationInSeconds = 1;
    private const float _minRadius = 1;
    private const float _maxRadius = 3;
    private const float _minSpeed = 1;
    private const float _maxSpeed = 3;

    public ReactTapGame CreateNewGame()
    {
        var cirles = CreateCircles();

        return new ReactTapGame
        {
            Circles = cirles,
            DurationInSeconds = _gameDurationInSeconds
        };
    }

    private IEnumerable<Circle> CreateCircles()
    {
        var radius = (float)(_minRadius + _random.NextDouble() * (_maxRadius - _minRadius));
        var speed = (float)(_minSpeed + _random.NextDouble() * (_maxSpeed - _minSpeed));
        var color = ColorPicker.GetRandomColor();

        yield return CreateCircle(radius, speed, color);
    }

    private Circle CreateCircle(float radius, float speed, Color color)
    {
        return new Circle
        {
            Radius = radius,
            Speed = speed,
            DurationInSeconds = _circleDurationInSeconds,
            Color = color,
            Path = CreateRandomPath()
        };
    }

    private IEnumerable<Vector2> CreateRandomPath()
    {
        var x = (float)_random.NextDouble();
        var y = (float)_random.NextDouble();
        var point = new Vector2(x, y);

        yield return point;
    }
}
