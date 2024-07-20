using UnityEngine;

public abstract class AbilityEffectSO : ScriptableObject {
    public bool AlreadyApplied = false;
    public Sprite icon;
    public abstract void ApplyEffect();
}