using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class QuickCalculationsController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _equationText;
    [SerializeField] Answer[] _answers;

    private PopupScript _popupScript;

    [Inject]
    private readonly IQuickCalculationsGameManager _quickCalculationsGameManager = new QuickCalculationsGameManager();
    private IScoreManager _scoreManager;

    private IEnumerator<Equation> _equationEnumerator;
    private int _score = 0;

    void Start()
    {
        var game = _quickCalculationsGameManager.CreateNewGame();
        _scoreManager = new ScoreManager(new ScoreRepository());
        _equationEnumerator = game.Equations.GetEnumerator();
        _equationEnumerator.MoveNext();
        RenderCurrentEquation();

        for (int i = 0; i < _answers.Length; i++)
        {
            _answers[i].Click += HandleAnswer;
        }

        Answer.IsClickable = true;

        _popupScript = gameObject.GetComponent<PopupScript>();
    }

    public void RenderCurrentEquation()
    {
        var equation = _equationEnumerator.Current;
        _equationText.text = equation.StringRepresentation();

        int[] components = new int[3]
        {
                equation.FirstNumber,
                equation.SecondNumber,
                equation.Answer
        };

        int questioned = components[(int)equation.HiddenPosition];
        int[] variants = equation.Variants.ToArray();

        for (int i = 0; i < _answers.Length; i++)
        {
            _answers[i].Set(variants[i].ToString(), questioned == variants[i]);
        }
    }

    public async void HandleAnswer(bool isCorrect, Answer answer)
    {
        Answer.IsClickable = false;
        var image = answer.gameObject.GetComponent<Image>();
        if (isCorrect)
        {
            await ShowSuccess(image);
            var currentEquation = _equationEnumerator.Current;
            _score += currentEquation.Reward;
            UpdateScore();
        }
        else
        {
            var imageCorrect = _answers
                .Where(a => a.IsCorrect)
                .Select(a => a.gameObject.GetComponent<Image>())
                .Single();

            await Task.WhenAll(ShowSuccess(imageCorrect), ShowFail(image));
        }

        if (_equationEnumerator.MoveNext())
        {
            RenderCurrentEquation();
            Answer.IsClickable = true;
        }
        else
        {
            _popupScript.Show(_score);
            _scoreManager.AddPoints(_score, typeof(QuickCalculationsGame));
        }
    }

    public async Task ShowSuccess(Image image)
    {
        Color startColor = new Color32(165, 255, 150, 0);
        Color endColor = new Color32(165, 255, 150, 255);
        image.color = startColor;
        DG.Tweening.Sequence colorSequence = DOTween.Sequence();
        colorSequence.Append(image.DOColor(endColor, 0.3f));
        colorSequence.Append(image.DOColor(startColor, 0.7f));

        await colorSequence.AsyncWaitForCompletion();
    }

    public async Task ShowFail(Image image)
    {
        Color startColor = new Color32(231, 141, 141, 0);
        Color endColor = new Color32(231, 141, 141, 255);
        image.color = startColor;
        DG.Tweening.Sequence colorSequence = DOTween.Sequence();
        colorSequence.Append(image.DOColor(endColor, 0.3f));
        colorSequence.Append(image.DOColor(startColor, 0.7f));

        await colorSequence.AsyncWaitForCompletion();
    }

    public void UpdateScore()
    {
        _scoreText.text = $"Очки: {_score}";
    }
}
