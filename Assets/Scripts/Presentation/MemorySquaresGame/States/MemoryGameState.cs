abstract public class MemoryGameState
{
    protected TileState tileState;
    protected IMemoryGameStateSwitcher stateSwitcher;

    public MemoryGameState(TileState state, IMemoryGameStateSwitcher switcher)
    {
        tileState = state;
        stateSwitcher = switcher;
    }

    public abstract void Start();

    public abstract void Stop();

    public abstract void OnTileClick(int index);
}