using UnityEngine;

public abstract class AbilityEffectSO : ScriptableObject {
    public bool AlreadyApplied = false;
    public abstract void ApplyEffect();
}