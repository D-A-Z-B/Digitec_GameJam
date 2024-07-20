public class RoomManager : MonoSingleton<RoomManager>
{
    private int _battleCount = 0;

    public int BattleCount {
        get => _battleCount;
        set => _battleCount = value;
    }
}
