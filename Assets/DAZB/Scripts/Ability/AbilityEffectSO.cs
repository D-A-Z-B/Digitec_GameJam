using UnityEngine;

public abstract class AbilityEffectSO : ScriptableObject {
    public bool AlreadyApplied = false;
    public virtual void ApplyEffect() {
        if (AlreadyApplied == true) return;
        AlreadyApplied = true;
    }
}