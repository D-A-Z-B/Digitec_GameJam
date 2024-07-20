using UnityEngine;

public class DinoEarthQuakeState : EnemyState<DinoStateEnum>
{
    public DinoEarthQuakeState(Enemy enemy, EnemyStateMachine<DinoStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private readonly int DownHash = Animator.StringToHash("Down");

    private Dino _dino;

    public override void Enter() {
        base.Enter();

        _dino = _enemy as Dino;
    }

    public override void UpdateState() {
        if(_endTriggerCalled) {
            _enemy.SetVelocity(0f, 20f);
            _enemy.AnimatorCompo.SetTrigger(DownHash);
            _enemy.StartDelayCallback(1.3f, Down);
        }
    }

    public override void Exit() {
        _enemy.lastAttackTime = Time.time;

        base.Exit();
    }

    private void Down() {
        _enemy.transform.localPosition = new Vector3(0, 30, 0);
        _enemy.SetVelocity(0f, -5f);
        _dino.warning.ShowRange(new Vector2(-3, 0), new Vector2(6, 30), 1);

        _enemy.StartDelayCallback(1f, () => {
            CameraManager.Instance.Shake(5, 0.5f);
            _stateMachine.ChangeState(DinoStateEnum.Chase);
        });
        _enemy.StartDelayCallback(1.7f, _dino.Meteor);
    }
}
