using UnityEngine;

public class DeadState : EnemyState<EnemyStateEnum>
{
    public DeadState(Enemy enemy, EnemyStateMachine<EnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }


}
