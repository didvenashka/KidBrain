using DG.Tweening;
using System.Collections;
using System.Linq;
using UnityEngine;

public class ShowSequenceState : MemoryGameState
{
    public ShowSequenceState(TileState state, IMemoryGameStateSwitcher switcher, TileScript[] tiles) : base(state, switcher) 
    {
        this.tiles = tiles;
    }
    TileScript[] tiles;

    public override void Start()
    {
        stateSwitcher.Coroutine(Show());
    }

    IEnumerator Show()
    {
        Sequence s = stateSwitcher.CurrentSequence();
        yield return new WaitForSeconds(1f);
        float duration = (float)s.DurationInSeconds / s.Order.Count();
        foreach (int tileIndex in s.Order)
        {
            yield return tiles[tileIndex].Animate(duration).WaitForCompletion();
        }
        stateSwitcher.SwitchState<RepeatSequenceState>();
    }

    public override void Stop()
    {

    }

    public override void OnTileClick(int index) { }
}