using UnityEngine;

[CreateAssetMenu(menuName = "SO/Ability/GlassCannon")]
public class AbilityGlassCannon : AbilityEffectSO {
    public int IncreaseAttackDamage;
    public int DecreaseHealth = 0;
    public override void ApplyEffect()
    {
        if (!AlreadyApplied) {
            AlreadyApplied = true;
            PlayerManager.Instance.Player.attackDamage += IncreaseAttackDamage;
            PlayerManager.Instance.Player.GetComponent<Health>().MaxHealth += DecreaseHealth;
        }
    }
}