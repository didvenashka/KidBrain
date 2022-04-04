using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TileScript : MonoBehaviour
{
    [SerializeField] Color activeColor;
    [SerializeField] Image img;
    [SerializeField] TileState state;

    public int Index { get; set; }

    Color inactiveColor = new Color32(229, 229, 229, 255);

    private void Start()
    {
        img.color = inactiveColor;
    }

    public void OnClick()
    {
        if (!state.CanClick) return;
        Animate(0.3f);
        state.Click(Index);
    }

    public Tween Animate(float duration)
    {
        DG.Tweening.Sequence colorSequence = DOTween.Sequence();
        colorSequence.Append(img.DOColor(activeColor, duration / 2));
        colorSequence.Append(img.DOColor(inactiveColor, duration / 2));
        return colorSequence;
    }
}
