using System;
using UnityEngine;
using UnityEngine.UI;

public class ImageHandler : MonoBehaviour
{
    [SerializeField] Image _image;
    public bool IsCorrect = false;
    public event Action<bool, ImageHandler> Click;
    public static bool IsClickable = true;

    public void Set(Color color, bool isCorrect)
    {
        _image.color = color;
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
