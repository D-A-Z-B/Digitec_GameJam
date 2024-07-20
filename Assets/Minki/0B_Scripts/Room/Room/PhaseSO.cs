using UnityEngine;

[System.Serializable]
public struct EnemySpawnSet {
    public Enemy enemy;
    public Transform position;
}

public class PhaseSO : MonoBehaviour
{
    public EnemySpawnSet[] enemySet;
}
