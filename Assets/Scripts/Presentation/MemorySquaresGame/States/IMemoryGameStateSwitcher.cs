using System.Collections;

public interface IMemoryGameStateSwitcher
{
    public Sequence CurrentSequence();
    public bool IsEnd();
    public void MoveNext();
    public void Coroutine(IEnumerator routine);
    public void SwitchState<T>() where T : MemoryGameState;
}
