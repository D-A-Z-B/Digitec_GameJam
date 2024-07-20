using UnityEngine;

public class BossRoom : Room
{
    [SerializeField] private Enemy _bossPrefab;
    [SerializeField] private Transform _bossSpawnPosition;

    private void Start() {
        OnActive += SpawnBoss;
    }

    private void SpawnBoss() {
        Enemy boss = Instantiate(_bossPrefab, _bossSpawnPosition.position, Quaternion.identity, transform);

        boss.HealthCompo.OnDead += ClearCheck;
    }

    private void ClearCheck() {
        if(nextRoom != null) Clear();
        else {
            Debug.Log("게임 끝!!");
        }
    }
}
