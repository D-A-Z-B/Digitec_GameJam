using UnityEngine;

public class AirplaneChaseState : ChaseState
{
    public AirplaneChaseState(Enemy enemy, EnemyStateMachine<EnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    protected override void Move() {
        if(!_enemy.CanAttack()) {
            _enemy.StopImmediately(true);
            return;
        }

        Vector2 direction = _playerHeadTrm.position - _enemy.transform.position;

        _enemy.SetVelocity(direction.normalized.x * _enemy.moveSpeed, direction.normalized.y * _enemy.moveSpeed);

        if(_enemy.CanAttack() && _enemy.attackDistance >= direction.magnitude) {
            _stateMachine.ChangeState(EnemyStateEnum.Attack);
        }
    }
}
