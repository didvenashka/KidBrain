using TMPro;
using UnityEngine;

public class PopupScript : MonoBehaviour
{
    [SerializeField] GameObject _popup;
    [SerializeField] TextMeshProUGUI _score;

    public void Show(int score)
    {
        _score.text = $"{score}/100";
        _popup.SetActive(true);
    }
}
