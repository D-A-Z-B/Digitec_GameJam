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

        _enemy.transform.rotation = Quaternion.Euler(0, _enemy.transform.eulerAngles.y, Mathf.Atan(direction.normalized.y / direction.normalized.x) * Mathf.Rad2Deg);

        if(_enemy.CanAttack() && _enemy.attackDistance >= direction.magnitude) {
            _stateMachine.ChangeState(EnemyStateEnum.Attack);
        }
    }
}
