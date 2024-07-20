using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Ability/Sensitive")]
public class AbilitySensitive : AbilityEffectSO
{
    public float IncreaseJustEvasionRange;
    public override void ApplyEffect()
    {
        if (!AlreadyApplied) {
            AlreadyApplied = true;
            PlayerManager.Instance.Head.JustEvasionCheckRange += IncreaseJustEvasionRange;
        }
    }
}