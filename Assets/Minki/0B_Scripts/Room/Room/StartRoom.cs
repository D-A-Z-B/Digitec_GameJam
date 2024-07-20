using System;

public class StartRoom : Room
{
    public event Action OnGameStart;

    public void GameStart() {
        Clear();
        OnGameStart?.Invoke();
    }
}
