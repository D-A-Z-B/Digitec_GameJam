using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AbilityManager : MonoSingleton<AbilityManager>
{
    [field: SerializeField] public List<AbilityEffectSO> PlayerAbilityEffectSOList { get; private set; } = new List<AbilityEffectSO>();
    public Transform uiPos;
    public List<UpgradeObject> abilityObjectPrefabs = new List<UpgradeObject>();
    public List<UpgradeObject> spawnedUpgradeObjects = new List<UpgradeObject>(); // List로 수정

    private Action _selectAction;

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.T))
    //     {
    //         ApplyEffect();
    //     }
    // }

    // private void Start()
    // {
    //     // Vector2[] vectorArr = {
    //     //     new Vector2(-5, 0),
    //     //     new Vector2(0, 0),
    //     //     new Vector2(5, 0),
    //     // };
    //     // SpawnAbilityObject(vectorArr);
    // }

    /// <summary>
    /// 지정된 위치에 능력 오브젝트를 생성합니다.
    /// </summary>
    /// <param name="spawnPos">생성할 위치 배열</param>
    public void SpawnAbilityObject(Vector2[] spawnPos, Action callback)
    {
        _selectAction = null;
        _selectAction = callback;
        int randNum;
        List<int> usedIndexes = new List<int>();

        for (int i = 0; i < spawnPos.Length; ++i)
        {
            // 가능한 인덱스를 찾습니다.
            do
            {
                randNum = Random.Range(0, abilityObjectPrefabs.Count);
            } while (usedIndexes.Contains(randNum));

            usedIndexes.Add(randNum);

            UpgradeObject obj = Instantiate(abilityObjectPrefabs[randNum], spawnPos[i], Quaternion.identity);
            obj.Index = i;
            obj.DescriptionPosition = uiPos;
            spawnedUpgradeObjects.Add(obj); // 리스트에 추가
        }
    }

    public void AddAbility(AbilityEffectSO so)
    {
        PlayerAbilityEffectSOList.Add(Instantiate(so));
        ApplyEffect();
    }

    public void SelectObject(int index) {
        for (int i = 0; i < 3; ++i) {
            if (i == index) {
                AddAbility(spawnedUpgradeObjects[i].UpgradeSO.abilityEffectSO);
                spawnedUpgradeObjects[i].BigExplosion();
                Destroy(spawnedUpgradeObjects[i].gameObject);
            }
            else {
                spawnedUpgradeObjects[i].SmallExplosion();
                Destroy(spawnedUpgradeObjects[i].gameObject);
            }
        }
        spawnedUpgradeObjects.Clear();
        _selectAction?.Invoke();
    }

    private void ApplyEffect()
    {
        foreach (var iter in PlayerAbilityEffectSOList)
        {
            iter.ApplyEffect();
        }
    }
}
