using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuickEyeController : MonoBehaviour
{
    [SerializeField] Image _mainImage;
    [SerializeField] ImageHandler[] _imageVariants;
    [SerializeField] TextMeshProUGUI _scoreText;

    private IQuickEyeGameManager _quickEyeGameManager = new QuickEyeGameManager();
    private IScoreManager _scoreManager;
    private PopupScript _popupScript;

    private IEnumerator<Match> _matchEnumerator;
    private int _score = 0;

    private void Start()
    {
        _scoreManager = new ScoreManager(new ScoreRepository());
        _popupScript = gameObject.GetComponent<PopupScript>();

        var game = _quickEyeGameManager.CreateNewGame();
        _matchEnumerator = game.Matches.GetEnumerator();
        _matchEnumerator.MoveNext();
        RenderCurrentMatch();

        for (int i = 0; i < _imageVariants.Length; i++)
        {
            _imageVariants[i].Click += HandleAnswer;
        }
    }
    private void RenderCurrentMatch()
    {
        var currentMatch = _matchEnumerator.Current;
        var mainPictureId = currentMatch.MainPictureId;
        var mainPictureColor = ColorPicker.Colors[mainPictureId];
        _mainImage.color = new Color32(mainPictureColor.R, mainPictureColor.G, mainPictureColor.B, mainPictureColor.A);

        int[] variants = currentMatch.VariantsIds.ToArray();

        for (int i = 0; i < variants.Length; i++)
        {
            var variantColor = ColorPicker.Colors[variants[i]];
            var unityColor = new Color32(variantColor.R, variantColor.G, variantColor.B, variantColor.A);
            _imageVariants[i].Set(unityColor, mainPictureId == variants[i]);
        }
    }

    private async void HandleAnswer(bool isCorrect, ImageHandler imageVariant)
    {
        ImageHandler.IsClickable = false;
        var image = imageVariant.gameObject.GetComponent<Image>();
        if (isCorrect)
        {
            await ShowSuccess(image);
            var currentMatch = _matchEnumerator.Current;
            _score += currentMatch.Reward;
            UpdateScore();
        }
        else
        {
            var imageCorrect = _imageVariants
                .Where(a => a.IsCorrect)
                .Select(a => a.gameObject.GetComponent<Image>())
                .Single();

            await Task.WhenAll(ShowSuccess(imageCorrect), ShowFail(image));
        }

        if (_matchEnumerator.MoveNext())
        {
            RenderCurrentMatch();
            ImageHandler.IsClickable = true;
        }
        else
        {
            _popupScript.Show(_score);
            _scoreManager.AddPoints(_score, typeof(QuickEyeGame));
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

