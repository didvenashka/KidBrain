using System;
using TMPro;
using UnityEngine;

public class Answer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI answerText;
    bool correct = false;
    public event Action<bool> Click;

    public void Set(string text, bool isCorrect)
    {
        answerText.text = text;
        correct = isCorrect;
    }

    public void OnClick()
    {
        Click?.Invoke(correct);
    }
}
