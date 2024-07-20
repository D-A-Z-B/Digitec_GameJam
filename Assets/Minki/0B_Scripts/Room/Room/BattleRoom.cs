using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRoom : Room
{
    [SerializeField] private PhaseSO[] _phases;

    private int _currentPhase = 0;
    private bool _phaseClear = false;

    private List<Enemy> _enemies = new List<Enemy>();

    private void Start() {
        OnActive += Phase;
    }

    private void Phase() {
        StartCoroutine(PhaseCoroutine());
    }

    private IEnumerator PhaseCoroutine() {
        yield return new WaitForSeconds(2f);

        for(int i = 0; i < 3; ++i) {
            Generate();

            yield return new WaitUntil(() => _phaseClear);
            yield return new WaitForSeconds(1f);
        }

        ++RoomManager.Instance.BattleCount;

        if(RoomManager.Instance.BattleCount == 2) {
            Debug.Log("증강");
            Clear();
        }
        else Clear();
    }

    public void Generate() {
        EnemySpawnSet[] enemySet = _phases[_currentPhase].enemySet;

        for(int i = 0; i < enemySet.Length; ++i) {
            Enemy enemy = Instantiate(enemySet[i].enemy, enemySet[i].position.position, Quaternion.identity, transform);
            enemy.HealthCompo.OnDead += () => _enemies.Remove(enemy);
            enemy.HealthCompo.OnDead += CheckClear;

            _enemies.Add(enemy);
        }

        ++_currentPhase;
    }

    private void CheckClear() {
        _phaseClear = true;
    }
}
