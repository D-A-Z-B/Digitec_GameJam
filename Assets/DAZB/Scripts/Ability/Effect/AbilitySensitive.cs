using UnityEngine;

public class AbilitySensitive : AbilityEffectSO
{
    public float IncreaseJustEvasionRange;
    public override void ApplyEffect()
    {
        base.ApplyEffect();
        PlayerManager.Instance.Head.defaultJustEvasionCheckRange += IncreaseJustEvasionRange;
    }
}