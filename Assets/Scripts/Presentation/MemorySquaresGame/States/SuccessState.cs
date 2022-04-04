using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SuccessState : MemoryGameState
{
    Image resultSignal;
    public SuccessState(TileState state, IMemoryGameStateSwitcher switcher, Image img) : base(state, switcher) 
    {
        resultSignal = img;
    }

    public override void Start()
    {
        stateSwitcher.Coroutine(ShowSuccess());
    }

    IEnumerator ShowSuccess()
    {
        Color startColor = new Color32(165, 255, 150, 0);
        Color endColor = new Color32(165, 255, 150, 255);
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
