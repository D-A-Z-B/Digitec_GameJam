using UnityEngine;

public class RoomManager : MonoSingleton<RoomManager>
{
    private int _battleCount = 0;

    public int BattleCount {
        get => _battleCount;
        set => _battleCount = value;
    }

    [SerializeField] private RoomGenerator _generator;
    [SerializeField] private InputReader _inputReader;

    private void Awake() {
        _generator.GenerateAll();
        _inputReader.PlayerInputDisable();
    }

    public void GameStart() {
        _generator.ActiveFirstRoom();
        _inputReader.PlayerInputEnable();
    }
}
