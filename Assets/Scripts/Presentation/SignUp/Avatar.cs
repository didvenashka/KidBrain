using System;
using UnityEngine;
using UnityEngine.UI;

public class Avatar : MonoBehaviour
{
    [SerializeField] Image _avatar;
    [SerializeField] GameObject _frame;
    int _index = -1;
    public event Action<int> Click;

    public void Set(Sprite avatar, int index)
    {
        _avatar.sprite = avatar;
        _index = index;
    }

    public void Select()
    {
        _frame.SetActive(true);
    }

    public void DeSelect()
    {
        _frame.SetActive(false);
    }

    public void OnClick()
    {
        Click?.Invoke(_index);
    }
}
