using System.Collections;
using System.Collections.Generic;
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
}
