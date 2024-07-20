using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(menuName = "SO/Ability/Blind")]
public class AbilityBlind : AbilityEffectSO
{
    public int IncreaseAttackDamage;
    
    public override void ApplyEffect()
    {
        if (!AlreadyApplied) {
            AlreadyApplied = true;
            PlayerManager.Instance.Player.attackDamage += IncreaseAttackDamage;
            PlayerManager.Instance.Player.transform.Find("Light 2D").gameObject.SetActive(true);
        }
    }
}