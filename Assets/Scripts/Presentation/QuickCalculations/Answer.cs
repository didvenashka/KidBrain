using System;
using TMPro;
using UnityEngine;

public class Answer : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI answerText;
    public bool IsCorrect = false;
    public event Action<bool, Answer> Click;
    public static bool IsClickable = true;

    public void Set(string text, bool isCorrect)
    {
        answerText.text = text;
        IsCorrect = isCorrect;
    }

    public void OnClick()
    {
        if (!IsClickable)
        {
            return;
        }

        Click?.Invoke(IsCorrect, this);
    }
}
