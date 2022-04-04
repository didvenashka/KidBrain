using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TileState")]
public class TileState : ScriptableObject
{
    public bool CanClick { get; set; } = false;

    public event Action<int> TileClick;

    public void Click(int index) => TileClick?.Invoke(index);
}
