using UnityEngine;

[CreateAssetMenu(menuName = "SO/Ability/Spike")]
public class AbilitySpike : AbilityEffectSO {
    public GameObject headObject;

    public override void ApplyEffect()
    {
        if (!AlreadyApplied) {
            AlreadyApplied = true;
            GameObject go = Instantiate(headObject);
            Head newObjectHead =  go.GetComponent<Head>();
            newObjectHead.attackCoolDown = PlayerManager.Instance.Head.attackCoolDown;
            newObjectHead.AbilityReignite = PlayerManager.Instance.Head.AbilityReignite;
            newObjectHead.AbilityComeBack = PlayerManager.Instance.Head.AbilityComeBack;
            newObjectHead.AbilityGravitation = PlayerManager.Instance.Head.AbilityGravitation;
            newObjectHead.AbilitySpark = PlayerManager.Instance.Head.AbilitySpark;
            newObjectHead.JustEvasionCheckRange = PlayerManager.Instance.Head.JustEvasionCheckRange;
            newObjectHead.returnLayer = PlayerManager.Instance.Head.returnLayer;
            newObjectHead.attackRange = PlayerManager.Instance.Head.attackRange;
            newObjectHead.ShockWave = PlayerManager.Instance.Head.ShockWave;
            Destroy(PlayerManager.Instance.Head.gameObject);
            go.transform.SetParent(PlayerManager.Instance.Player.transform.parent);
        }
    }
}