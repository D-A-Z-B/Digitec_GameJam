using UnityEngine;

public class DinoGroggyState : EnemyState<DinoStateEnum>
{
    public DinoGroggyState(Enemy enemy, EnemyStateMachine<DinoStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    public override void Enter() {
        base.Enter();

        _enemy.StartDelayCallback(10f, () => _stateMachine.ChangeState(DinoStateEnum.Chase));
    }
}
