using UnityEngine;

public class DeadState : EnemyState<EnemyStateEnum>
{
    public DeadState(Enemy enemy, EnemyStateMachine<EnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    public override void Enter() {
        base.Enter();

        _enemy.StopImmediately(false);

        _enemy.ColliderCompo.enabled = false;
    }

    public override void UpdateState() {
        if(_endTriggerCalled) {
            GameObject.Destroy(_enemy.gameObject);
        }
    }
}
