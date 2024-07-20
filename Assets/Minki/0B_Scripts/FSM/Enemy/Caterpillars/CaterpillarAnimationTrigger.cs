using UnityEngine;

public class CaterpillarAnimationTrigger : EnemyAnimationTrigger
{
    private Caterpillar _catepillar;

    protected override void Awake() {
        base.Awake();

        _catepillar = _enemy as Caterpillar;
    }

    public void AnimationMoveTrigger() {
        _catepillar.moveTriggerCalled = true;
    }
}
