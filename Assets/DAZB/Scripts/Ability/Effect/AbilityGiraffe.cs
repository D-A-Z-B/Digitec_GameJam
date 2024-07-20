using UnityEngine;

[CreateAssetMenu(menuName = "SO/Ability/Giraffe")]
public class AbilityGiraffe : AbilityEffectSO
{
    public int IncreaseRange;
    public override void ApplyEffect()
    {
        if (!AlreadyApplied) {
            AlreadyApplied = true;
            PlayerManager.Instance.Head.attackRange += IncreaseRange;
        }
    }
}