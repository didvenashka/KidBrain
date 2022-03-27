using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CircleScript : MonoBehaviour
{
    Circle circle;
    IEnumerable<Vector2> positions;
    Vector2 currentPos;
    Tween currentAnimation;

    public void Init(Circle c)
    {
        circle = c;
        transform.localScale = Vector3.one * circle.Radius;
        positions = circle.Path.Select(vec => new Vector2(vec.X, vec.Y));
        currentPos = positions.First();
        transform.position = currentPos;

        StartCoroutine(Move());
    }

    private void OnDestroy()
    {
        currentAnimation?.Kill();
    }

    IEnumerator Move()
    {
        foreach (Vector2 pos in positions.Skip(1))
        {
            float duration = (pos * 2 - currentPos * 2).magnitude / circle.Speed;
            currentAnimation = transform.DOMove(pos, duration);
            currentPos = pos;
            yield return currentAnimation;
        }
        
    }
}
