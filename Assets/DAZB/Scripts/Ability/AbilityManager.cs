using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AbilityManager : MonoSingleton<AbilityManager>
{
    [field:SerializeField] public List<AbilityEffectSO> AbilityEffectSOList {get; private set;} = new List<AbilityEffectSO>();

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            ApplyEffect();
        }
    }
    
    public void AddAbility(AbilityEffectSO so) {
        AbilityEffectSOList.Add(Instantiate(so));
        ApplyEffect();
    }

    private void ApplyEffect() {
        foreach (var iter in AbilityEffectSOList) {
            iter.ApplyEffect();
        }
    }
}
