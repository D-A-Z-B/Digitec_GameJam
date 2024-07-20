using UnityEngine;

public class AbilityReignite : AbilityEffectSO {
    public float DecreaseCooldown;
    public override void ApplyEffect()
    {
        base.ApplyEffect();
        PlayerManager.Instance.Head.AbilityReignite = true;
        PlayerManager.Instance.Head.attackCoolDown -= DecreaseCooldown;

    }
}