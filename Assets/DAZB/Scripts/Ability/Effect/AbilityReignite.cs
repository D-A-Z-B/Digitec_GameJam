using UnityEngine;

[CreateAssetMenu(menuName = "SO/Ability/Reignite")]
public class AbilityReignite : AbilityEffectSO {
    public float DecreaseCooldown;
    public override void ApplyEffect()
    {
        if (!AlreadyApplied) {            
            PlayerManager.Instance.Head.AbilityReignite = true;
            PlayerManager.Instance.Head.attackCoolDown -= DecreaseCooldown;
        }
    }
}