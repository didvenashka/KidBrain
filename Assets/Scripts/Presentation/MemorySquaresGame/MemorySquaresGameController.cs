using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MemorySquaresGameController : MonoBehaviour, IMemoryGameStateSwitcher
{
    [SerializeField] TileScript[] tiles;
    [SerializeField] TileState tileSharedState;
    [SerializeField] Image resultSignal;
    [SerializeField] TextMeshProUGUI _scoreText;

    private PopupScript _popupScript;

    [Inject]
    private readonly IMemorySquaresGameManager _memorySquaresGameManager = new MemorySquaresGameManager();
    private IScoreManager _scoreManager;

    MemorySquaresGame game;

    List<Sequence> _sequenceList;
    int _currentSequenceIndex = 0;

    List<MemoryGameState> _states;
    MemoryGameState _currentState;

    private int _score = 0;

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

        _scoreManager = new ScoreManager(new ScoreRepository());
        _popupScript = gameObject.GetComponent<PopupScript>();
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
        MemoryGameState state = _states.FirstOrDefault(s => s is T);
        _currentState.Stop();
        _currentState = state;

        if (_currentState.GetType() == typeof(SuccessState))
        {
            var currentReward = _sequenceList[_currentSequenceIndex].Reward;
            _score += currentReward;
            UpdateScore();
        }

        _currentState.Start();
    }

    private void UpdateScore()
    {
        _scoreText.text = $"Очки: {_score}";
    }

    public bool IsEnd()
    {
        var isEnd = _currentSequenceIndex == _sequenceList.Count;

        if (isEnd)
        {
            _popupScript.Show(_score);
            _scoreManager.AddPoints(_score, typeof(MemorySquaresGame));
        }

        return isEnd;
    }

    public void MoveNext()
    {
        _currentSequenceIndex++;
    }
}
