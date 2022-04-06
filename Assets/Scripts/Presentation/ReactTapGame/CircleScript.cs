using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CircleScript : MonoBehaviour
{
    [SerializeField] RectTransform root;
    Circle circle;
    IEnumerable<Vector2> positions;
    Vector2 currentPos;
    Vector2 spawnSize;
    Tween currentAnimation;

    [SerializeField] Image _signalRing;

    public bool IsCircleCaught { get; private set; }
    public bool IsCircleResolved { get; private set; }

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

    public void OnClick()
    {
        CompleteByCapture(true);
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
        yield return ShowFailure();
        CompleteByCapture(false);
    }

    IEnumerator ShowFailure()
    {
        Color startColor = new Color32(231, 141, 141, 0);
        Color endColor = new Color32(231, 141, 141, 255);
        _signalRing.color = startColor;
        DG.Tweening.Sequence colorSequence = DOTween.Sequence();
        colorSequence.Append(_signalRing.DOColor(endColor, 0.3f));
        yield return colorSequence.WaitForCompletion();
    }

    void CompleteByCapture(bool isCaught)
    {
        IsCircleCaught = isCaught;
        IsCircleResolved = true;
    }
}
