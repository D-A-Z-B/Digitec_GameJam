using UnityEngine;

[CreateAssetMenu(menuName = "SO/Ability/Gravitation")]
public class AbilityGravitation : AbilityEffectSO
{
    public int multiples;
    public float DecreaseAttackSpeed;
    public float IncreaseReturnSpeed;
    public override void ApplyEffect()
    {
        if (!AlreadyApplied) {
            AlreadyApplied = true;
            PlayerManager.Instance.Head.AbilityGravitation = true;
            PlayerManager.Instance.Head.attackSpeed -= DecreaseAttackSpeed;
            PlayerManager.Instance.Head.returnSpeed += IncreaseReturnSpeed;
            PlayerManager.Instance.Player.returnDamage = PlayerManager.Instance.Player.attackDamage * multiples;
        }
    }
}