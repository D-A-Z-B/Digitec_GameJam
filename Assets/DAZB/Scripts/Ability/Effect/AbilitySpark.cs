using UnityEngine;

[CreateAssetMenu(menuName = "SO/Ability/Spark")]
public class AbilitySpark : AbilityEffectSO
{
    [SerializeField] private GameObject sparkPrefab;
    [SerializeField] private int count;
    public int IncreaseAttackCooldown;
    public override void ApplyEffect()
    {
        if (!AlreadyApplied) {
            AlreadyApplied = true;
            PlayerManager.Instance.Head.AbilitySpark = true;
            PlayerManager.Instance.Head.attackCoolDown -= IncreaseAttackCooldown;
            PlayerManager.Instance.Head.SparkEvent += SparkEventHandle;
        }
    }

    private void SparkEventHandle()
    {
        for (int i = 0; i < count; ++i) {
            Instantiate(sparkPrefab, PlayerManager.Instance.Head.transform.position, Quaternion.identity);
        }
    }
}