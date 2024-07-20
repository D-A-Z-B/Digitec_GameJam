using UnityEngine;

[CreateAssetMenu(menuName = "SO/Ability/ApShot")]
public class AbilityApShot : AbilityEffectSO
{
    public int DecreaseDamage;
    public float IncreaseAttackSpeed;
    public override void ApplyEffect()
    {
        if (!AlreadyApplied) {
            AlreadyApplied = true;
            PlayerManager.Instance.Head.AbilityApShot = true;
            PlayerManager.Instance.Player.attackDamage -= DecreaseDamage;
            PlayerManager.Instance.Head.attackSpeed += IncreaseAttackSpeed;
        }
    }
}