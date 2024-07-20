using UnityEngine;

public class RoomManager : MonoSingleton<RoomManager>
{
    private int _battleCount = 0;

    public int BattleCount {
        get => _battleCount;
        set => _battleCount = value;
    }

    [SerializeField] private RoomGenerator _generator;

    private void Awake() {
        _generator.GenerateAll();
    }

    public void GameStart() {
        _generator.ActiveFirstRoom();
    }
}
