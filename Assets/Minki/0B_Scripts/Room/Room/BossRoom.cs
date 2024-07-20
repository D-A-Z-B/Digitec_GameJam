using UnityEngine;

public class BossRoom : Room
{
    [SerializeField] private Enemy _bossPrefab;
    [SerializeField] private Transform _bossSpawnPosition;

    private void Start() {
        OnActive += SpawnBoss;
    }

    private void SpawnBoss() {
        Instantiate(_bossPrefab, _bossSpawnPosition.position, Quaternion.identity, transform);
    }
}
