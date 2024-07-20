using UnityEngine;

[CreateAssetMenu(menuName = "SO/Ability/GlassCannon")]
public class AbilityGlassCannon : AbilityEffectSO {
    public float IncreaseAttackDamage;
    public int DecreaseHealth = 0;
    public override void ApplyEffect()
    {
        if (!AlreadyApplied) {
            PlayerManager.Instance.Player.attackDamage += IncreaseAttackDamage;
            PlayerManager.Instance.Player.GetComponent<Health>().MaxHealth += DecreaseHealth;
        }
    }
}