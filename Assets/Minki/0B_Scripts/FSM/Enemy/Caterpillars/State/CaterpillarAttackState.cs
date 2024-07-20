public class CaterpillarAttackState : AttackState
{
    public CaterpillarAttackState(Enemy enemy, EnemyStateMachine<EnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    public override void Enter() {
        base.Enter();

        _enemy.Attack();
    }
}
