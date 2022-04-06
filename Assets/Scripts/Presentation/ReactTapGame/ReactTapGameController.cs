using System.Collections;
using TMPro;
using UnityEngine;

public class ReactTapGameController : MonoBehaviour
{
    [SerializeField] CircleScript _prefab;
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _timerText;
    [SerializeField] RectTransform _spawnBox;

    private readonly IReactTapGameManager _reactTapGameManager = new ReactTapGameManager();
    private IScoreManager _scoreManager;

    ReactTapGame game;

    Vector2 spawnSize;
    bool active;

    private PopupScript _popupScript;
    private int _score = 0;

    void Start()
    {
        game = _reactTapGameManager.CreateNewGame();
        active = true;
        StartCoroutine(Move());
        StartCoroutine(StartTimer());
        _popupScript = gameObject.GetComponent<PopupScript>();
        _scoreManager = new ScoreManager(new ScoreRepository());
    }

    IEnumerator Move()
    {
        yield return new WaitForEndOfFrame();
        spawnSize = _spawnBox.rect.size;
        foreach (Circle circle in game.Circles)
        {
            if (!active) yield break;
            CircleScript circleScript = Instantiate(_prefab, _spawnBox);
            circleScript.Init(circle, spawnSize);
            yield return new WaitUntil(() => circleScript.IsCircleResolved);

            if (circleScript.IsCircleCaught)
            {
                var reward = circle.Reward;
                _score += reward;
                UpdateScore();
            }

            Destroy(circleScript.gameObject);
        }
    }

    IEnumerator StartTimer()
    {
        for (int i = game.DurationInSeconds - 1; i >= 0; i--)
        {
            yield return new WaitForSeconds(1);
            UpdateTimer(i);
        }
        EndGame();
    }

    private void UpdateScore()
    {
        _score = _score > 100 ? 100 : _score;
        _scoreText.text = $"Очки: {_score}";

        if (_score == 100)
        {
            EndGame();
        }

    }

    private void UpdateTimer(int secondsLeft)
    {
        _timerText.text = $"0:{secondsLeft}";
    }

    private void EndGame()
    {
        active = false;
        StopAllCoroutines();
        _popupScript.Show(_score);
        _scoreManager.AddPoints(_score, typeof(ReactTapGame));
    }
}
