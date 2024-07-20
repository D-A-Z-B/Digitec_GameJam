using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoSingleton<AbilityManager>
{
    [field:SerializeField] public List<AbilityEffectSO> AbilityEffectSOList {get; private set;} = new List<AbilityEffectSO>();
    
    public void AddAbility(AbilityEffectSO so) {
        AbilityEffectSOList.Add(so);
        ApplyEffect();
    }

    private void ApplyEffect() {
        foreach (var iter in AbilityEffectSOList) {
            iter.ApplyEffect();
        }
    }
}
