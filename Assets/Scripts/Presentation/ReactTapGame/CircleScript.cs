using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CircleScript : MonoBehaviour
{
    [SerializeField] RectTransform root;
    Circle circle;
    IEnumerable<Vector2> positions;
    Vector2 currentPos;
    Vector2 spawnSize;
    Tween currentAnimation;

    bool done = false;
    public int Value { get; private set; } = 0;

    public void Init(Circle c, Vector2 spawnSize)
    {
        circle = c;
        this.spawnSize = spawnSize;
        transform.localScale = Vector3.one * circle.Radius;
        positions = circle.Path.Select(vec => new Vector2(vec.X, -vec.Y));
        currentPos = positions.First();
        root.anchoredPosition = currentPos * spawnSize;

        StartCoroutine(Move());
        StartCoroutine(Timeout());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        currentAnimation?.Kill();
    }

    public bool IsDone() => done;

    public void OnClick()
    {
        DoneWithResult(1);
    }

    IEnumerator Move()
    {
        foreach (Vector2 pos in positions.Skip(1))
        {
            float duration = (pos - currentPos).magnitude / circle.Speed;
            currentAnimation = root.DOAnchorPos(pos * spawnSize, duration);
            currentPos = pos;
            yield return currentAnimation.WaitForCompletion();
        }
        
    }

    IEnumerator Timeout()
    {
        yield return new WaitForSeconds(circle.DurationInSeconds);
        DoneWithResult(0);
    }

    void DoneWithResult(int result)
    {
        Value = result;
        done = true;
    }
}
