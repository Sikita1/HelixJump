using System;

public class FinishPlatform : Platform
{
    public event Action GameWin;

    public void GameWinner() =>
        GameWin?.Invoke();
}
