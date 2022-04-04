using System.Collections.Generic;
using System.Linq;
public class RepeatSequenceState : MemoryGameState
{
    public RepeatSequenceState(TileState state, IMemoryGameStateSwitcher switcher) : base(state, switcher) { }

    int _currentIndex = 0;
    List<int> sequenceAsList;
    public override void Start()
    {
        tileState.CanClick = true;
        _currentIndex = 0;
        sequenceAsList = stateSwitcher.CurrentSequence().Order.ToList();
    }

    public override void Stop()
    {
        tileState.CanClick = false;
    }

    public override void OnTileClick(int index)
    {
        if (index == sequenceAsList[_currentIndex])
        {
            _currentIndex++;
            if (_currentIndex == sequenceAsList.Count)
                stateSwitcher.SwitchState<SuccessState>();
        }
        else
        {
            stateSwitcher.SwitchState<FailState>();
        }
    }
}