using UnityEngine;

[CreateAssetMenu(menuName = "SO/Ability/Spark")]
public class AbilitySpark : AbilityEffectSO
{
    [SerializeField] private GameObject sparkPrefab;
    [SerializeField] private int count;
    public override void ApplyEffect()
    {
        if (!AlreadyApplied) {
            AlreadyApplied = true;
        }
    }
    
    
}