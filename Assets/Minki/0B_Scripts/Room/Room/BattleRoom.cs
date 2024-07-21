using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleRoom : Room
{
    [System.Serializable]
    struct EnemySpawnSet {
        public Enemy enemy;
        public Transform position;
    }

    [System.Serializable]
    struct EnemySet {
        public EnemySpawnSet[] spawnSet;
    }

    [SerializeField] private EnemySet[] _phases;
    [SerializeField] private GameObject _upgradeObjects;

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

            ++_currentPhase;
            _phaseClear = false;
        }

        ++RoomManager.Instance.BattleCount;

        if(RoomManager.Instance.BattleCount == 1) {
            foreach(Transform upgradeObject in _upgradeObjects.transform) {
                upgradeObject.GetComponent<UpgradeObject>().OnInteractEnd += Clear;
            }
            _upgradeObjects.SetActive(true);
        }
        else Clear();
    }

    public void Generate() {
        EnemySpawnSet[] enemySet = _phases[_currentPhase].spawnSet;

        for(int i = 0; i < enemySet.Length; ++i) {
            Enemy enemy = Instantiate(enemySet[i].enemy, enemySet[i].position.position, Quaternion.identity, transform);
            enemy.HealthCompo.OnDead += () => _enemies.Remove(enemy);
            enemy.HealthCompo.OnDead += CheckClear;

            _enemies.Add(enemy);
        }
    }

    private void CheckClear() {
        if(_enemies.Count <= 1)
            _phaseClear = true;
    }
}
