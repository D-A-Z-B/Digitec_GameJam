using UnityEngine;

public class DinoSpitState : EnemyState<DinoStateEnum>
{
    public DinoSpitState(Enemy enemy, EnemyStateMachine<DinoStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    public override void UpdateState() {
        if(_endTriggerCalled) {
            _stateMachine.ChangeState(DinoStateEnum.Chase);
        }
    }

    public override void Exit() {
        _enemy.lastAttackTime = Time.time;

        base.Exit();
    }
}
