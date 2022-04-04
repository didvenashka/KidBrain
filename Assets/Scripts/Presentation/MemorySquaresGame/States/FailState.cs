using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FailState : MemoryGameState
{
    Image resultSignal;
    public FailState(TileState state, IMemoryGameStateSwitcher switcher, Image img) : base(state, switcher) { resultSignal = img; }

    public override void Start()
    {
        stateSwitcher.Coroutine(ShowFail());
    }

    IEnumerator ShowFail()
    {
        Color startColor = new Color32(231, 141, 141, 0);
        Color endColor = new Color32(231, 141, 141, 255);
        resultSignal.color = startColor;
        DG.Tweening.Sequence colorSequence = DOTween.Sequence();
        colorSequence.Append(resultSignal.DOColor(endColor, 0.3f));
        colorSequence.Append(resultSignal.DOColor(startColor, 0.3f));
        yield return colorSequence.WaitForCompletion();
        stateSwitcher.MoveNext();
        if (stateSwitcher.IsEnd())
        {

        }
        else
        {
            stateSwitcher.SwitchState<ShowSequenceState>();
        }
    }

    public override void Stop()
    {

    }

    public override void OnTileClick(int index) { }
}