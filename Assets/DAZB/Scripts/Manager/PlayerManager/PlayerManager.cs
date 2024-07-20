using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    private Player player;
    public Player Player {
        get {
            if (player == null) {
                player = FindObjectOfType<Player>();
                if (player == null) {
                    Debug.LogError("Does not exist player");
                }
            }
            return player;
        }
    }

    private Head head;
    public Head Head {
        get {
            if (head == null) {
                head = FindObjectOfType<Head>();
                if (head == null) {
                    Debug.Log("Does not exist player");
                }
            }
            return head;
        }
    }
}
