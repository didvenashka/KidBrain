using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MemorySquaresGameController : MonoBehaviour, IMemoryGameStateSwitcher
{
    [SerializeField] TileScript[] tiles;
    [SerializeField] TileState tileSharedState;
    [SerializeField] Image resultSignal;

    [Inject]
    private readonly IMemorySquaresGameManager _memorySquaresGameManager = new MemorySquaresGameManager();

    MemorySquaresGame game;

    List<Sequence> _sequenceList;
    int _currentSequenceIndex = 0;

    List<MemoryGameState> _states;
    MemoryGameState _currentState;

    void Start()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].Index = i;
        }
        tileSharedState.TileClick += TileClicked;
        game = _memorySquaresGameManager.CreateNewGame();
        _sequenceList = game.Sequences.ToList();
        _states = new List<MemoryGameState>()
        {
            new ShowSequenceState(tileSharedState, this, tiles),
            new RepeatSequenceState(tileSharedState, this),
            new SuccessState(tileSharedState, this, resultSignal),
            new FailState(tileSharedState, this, resultSignal)
        };
        _currentState = _states[0];
        _currentState.Start();
    }

    void OnDestroy()
    {
        tileSharedState.TileClick -= TileClicked;
    }

    public void TileClicked(int index)
    {
        _currentState.OnTileClick(index);
    }

    public Sequence CurrentSequence()
    {
        return _sequenceList[_currentSequenceIndex];
    }

    public void Coroutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }

    public void SwitchState<T>() where T : MemoryGameState
    {
        print(_currentState.GetType().Name);
        MemoryGameState state = _states.FirstOrDefault(s => s is T);
        _currentState.Stop();
        _currentState = state;
        _currentState.Start();
    }

    public bool IsEnd() => _currentSequenceIndex == _sequenceList.Count;

    public void MoveNext()
    {
        _currentSequenceIndex++;
    }
}
