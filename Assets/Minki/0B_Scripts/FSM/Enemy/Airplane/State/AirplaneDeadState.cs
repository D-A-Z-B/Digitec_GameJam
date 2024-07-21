using UnityEngine;

public class AirplaneDeadState : DeadState
{
    public AirplaneDeadState(Enemy enemy, EnemyStateMachine<EnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    public override void Enter() {
        base.Enter();

        GameObject.Destroy(_enemy.gameObject);
    }
}
