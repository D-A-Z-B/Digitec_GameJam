using UnityEngine;

[CreateAssetMenu(menuName = "SO/Ability/ComeBack")]
public class AbilityComeBack : AbilityEffectSO
{
    public int IncreaseRange;
    public override void ApplyEffect()
    {
        if (!AlreadyApplied) {
            AlreadyApplied = true;
            PlayerManager.Instance.Head.attackRange += IncreaseRange;
            PlayerManager.Instance.Head.AbilityComeBack = true;
        }
    }
}