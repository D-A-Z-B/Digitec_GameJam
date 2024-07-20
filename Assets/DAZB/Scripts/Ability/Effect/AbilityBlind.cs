using UnityEngine;

public class AbilityBlind : AbilityEffectSO
{
    public override void ApplyEffect()
    {
        if (!AlreadyApplied) {
            AlreadyApplied = true;
        }
    }
}