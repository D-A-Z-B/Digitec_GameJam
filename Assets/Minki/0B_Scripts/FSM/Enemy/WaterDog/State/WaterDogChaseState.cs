public class WaterDogChaseState : ChaseState
{
    public WaterDogChaseState(Enemy enemy, EnemyStateMachine<EnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    protected override void Move() {
        _enemy.FlipController(_playerHeadTrm.position.x - _enemy.transform.position.x);
        _enemy.SetVelocity(_enemy.FacingDirection * _enemy.moveSpeed, _enemy.RigidbodyCompo.velocity.y);
    }
}
