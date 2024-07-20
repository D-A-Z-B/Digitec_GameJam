using System.Collections.Generic;
using UnityEngine;

public class BattleRoom : Room
{
    [System.Serializable]
    private struct EnemySpawnSet {
        public Enemy enemy;
        public Transform position;
    }

    [SerializeField] private EnemySpawnSet[] _enemySet;

    private List<Enemy> _enemies = new List<Enemy>();

    public void Generate() {
        for(int i = 0; i < _enemySet.Length; ++i) {
            Enemy enemy = Instantiate(_enemySet[i].enemy, _enemySet[i].position.position, Quaternion.identity, transform);
            enemy.HealthCompo.OnDead += () => _enemies.Remove(enemy);
            enemy.HealthCompo.OnDead += CheckClear;

            _enemies.Add(enemy);
        }
    }

    private void CheckClear() {
        if(_enemies.Count == 0) Clear();
    }
}
