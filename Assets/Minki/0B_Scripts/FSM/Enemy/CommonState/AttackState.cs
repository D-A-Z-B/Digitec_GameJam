using UnityEngine;

public class AttackState : EnemyState<EnemyStateEnum>
{
    public AttackState(Enemy enemy, EnemyStateMachine<EnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    public override void UpdateState() {
        if(_endTriggerCalled) {
            _stateMachine.ChangeState(EnemyStateEnum.Chase);
        }
    }

    public override void Exit() {
        _enemy.lastAttackTime = Time.time;

        base.Exit();
    }
}
